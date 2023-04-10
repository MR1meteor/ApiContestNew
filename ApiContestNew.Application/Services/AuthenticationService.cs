using ApiContestNew.Core.Interfaces.Repositories;
using ApiContestNew.Core.Interfaces.Services;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Responses;

namespace ApiContestNew.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IAccountRepository _accountRepository;

        public AuthenticationService(
            IAuthenticationRepository authenticationRepository,
            IAccountRepository accountRepository)
        {
            _authenticationRepository = authenticationRepository;
            _accountRepository = accountRepository;
        }

        async public Task<ServiceResponse<Account>> Register(Account account)
        {
            if (!account.IsValidWithoutId())
            {
                return new ServiceResponse400<Account>();
            }

            var equalAccount = await _accountRepository.GetAccountByEmailAsync(account.Email);
            if (equalAccount != null)
            {
                return new ServiceResponse409<Account>();
            }

            var newAccount = await _authenticationRepository.Register(account);

            return new ServiceResponse201<Account>(data: newAccount);
        }
    }
}
