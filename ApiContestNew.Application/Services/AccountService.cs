using ApiContestNew.Core.Interfaces.Repositories;
using ApiContestNew.Core.Interfaces.Services;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Filters;
using ApiContestNew.Core.Models.Responses;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ApiContestNew.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public AccountService(
            IAccountRepository accountRepository,
            IHttpContextAccessor contextAccessor)
        {
            _accountRepository = accountRepository;
            _contextAccessor = contextAccessor;
        }

        async public Task<ServiceResponse<Account>> GetAccountAsync(int id)
        {
            if (id <= 0)
            {
                return new ServiceResponse400<Account>();
            }

            var account = await _accountRepository.GetAccountByIdAsync(id);

            var claims = _contextAccessor.HttpContext.User.Claims;
            var authorizedEmail = claims.Where(c => c.Type == ClaimTypes.Email)
                .Select(c => c.Value).SingleOrDefault();
            var authorizedRole = claims.Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value).SingleOrDefault();

            if (account == null && authorizedRole != "ADMIN" ||
                account != null && authorizedRole != "ADMIN" &&
                authorizedEmail != account.Email)
            {
                return new ServiceResponse403<Account>();
            }

            if (account == null)
            {
                return new ServiceResponse404<Account>();
            }

            return new ServiceResponse200<Account>(data: account);
        }

        async public Task<ServiceResponse<List<Account>>> GetAccountsAsync(AccountFilter accountFilter)
        {
            if (!accountFilter.IsValid())
            {
                return new ServiceResponse400<List<Account>>();
            }

            var accounts = await _accountRepository.GetAccountsByFilterAsync(accountFilter);

            return new ServiceResponse200<List<Account>>(data: accounts);
        }

        async public Task<ServiceResponse<Account>> AddAccountAsync(Account account)
        {
            var roles = new[] { "ADMIN", "CHIPPER", "USER" };

            if (!account.IsValidWithoutId() ||
                !roles.Contains(account.Role))
            {
                return new ServiceResponse400<Account>();
            }

            var equalAccount = await _accountRepository.GetAccountByEmailAsync(account.Email);
            if (equalAccount != null)
            {
                return new ServiceResponse409<Account>();
            }

            var newAccount = await _accountRepository.AddAccountAsync(account);

            return new ServiceResponse201<Account>(data: newAccount);
        }

        async public Task<ServiceResponse<Account>> UpdateAccountAsync(int id, Account account)
        {
            var roles = new[] { "ADMIN", "CHIPPER", "USER" };

            account.Id = id;
            if (!account.IsValid() ||
                !roles.Contains(account.Role))
            {
                return new ServiceResponse400<Account>();
            }

            var editableAccount = await _accountRepository.GetAccountByIdAsync(id);
            var claims = _contextAccessor.HttpContext.User.Claims;
            var authorizedEmail = claims.Where(c => c.Type == ClaimTypes.Email)
                .Select(c => c.Value).SingleOrDefault();
            var authorizedRole = claims.Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value).SingleOrDefault();

            if (editableAccount == null && authorizedRole != "ADMIN" ||
                editableAccount != null && authorizedRole != "ADMIN" &&
                editableAccount.Email != authorizedEmail)
            {
                return new ServiceResponse403<Account>();
            }

            if (editableAccount == null)
            {
                return new ServiceResponse404<Account>();
            }

            var equalAccount = await _accountRepository.GetAccountByEmailAsync(account.Email);
            if (equalAccount != null && equalAccount != editableAccount)
            {
                return new ServiceResponse409<Account>();
            }

            var editedAccount = await _accountRepository.UpdateAccountAsync(account);

            return new ServiceResponse200<Account>(data: editedAccount);
        }

        async public Task<ServiceResponse<Account>> DeleteAccountAsync(int id)
        {
            if (id <= 0)
            {
                return new ServiceResponse400<Account>();
            }

            var account = await _accountRepository.GetAccountByIdAsync(id);

            var claims = _contextAccessor.HttpContext.User.Claims;
            var authorizedEmail = claims.Where(c => c.Type == ClaimTypes.Email)
                .Select(c => c.Value).SingleOrDefault();
            var authorizedRole = claims.Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value).SingleOrDefault();

            if (account == null && authorizedRole != "ADMIN" ||
                account != null && authorizedRole != "ADMIN" &&
                account.Email != authorizedEmail)
            {
                return new ServiceResponse403<Account>();
            }

            if (account == null)
            {
                return new ServiceResponse404<Account>();
            }

            if (account.ChippedAnimals.Count != 0)
            {
                return new ServiceResponse400<Account>();
            }

            await _accountRepository.DeleteAccountAsync(account);

            return new ServiceResponse200<Account>();
        }
    }
}
