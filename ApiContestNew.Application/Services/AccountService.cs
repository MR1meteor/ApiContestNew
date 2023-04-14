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

        async public Task<ServiceResponse<Account>> UpdateAccountAsync(int id, Account account)
        {
            account.Id = id;
            if (!account.IsValid())
            {
                return new ServiceResponse400<Account>();
            }

            var editableAccount = await _accountRepository.GetAccountByIdAsync(id);
            var claims = _contextAccessor.HttpContext.User.Claims;
            
            if (editableAccount == null || claims.Count() <= 0)
            {
                return new ServiceResponse403<Account>();
            }

            var authorizedEmail = claims.Where(c => c.Type == ClaimTypes.Email)
                .Select(c => c.Value).SingleOrDefault();

            if (editableAccount.Email != authorizedEmail)
            {
                return new ServiceResponse403<Account>();
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
            var authorizedEmail = _contextAccessor.HttpContext.User
                .Claims.Where(c => c.Type == ClaimTypes.Email)
                .Select(c => c.Value).SingleOrDefault();

            if (account == null || account.Email != authorizedEmail)
            {
                return new ServiceResponse403<Account>();
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
