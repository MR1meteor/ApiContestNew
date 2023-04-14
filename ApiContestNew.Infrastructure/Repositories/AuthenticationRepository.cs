using ApiContestNew.Core.Interfaces.Repositories;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Specifications.Account;
using ApiContestNew.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace ApiContestNew.Infrastructure.Repositories
{
    public class AuthenticationRepository : BaseRepository<Account>, IAuthenticationRepository
    {
        private readonly IAccountRepository _accountRepository;

        public AuthenticationRepository(
            DataContext dataContext,
            IAccountRepository accountRepository)
            : base(dataContext)
        {
            _accountRepository = accountRepository;
        }

        async public Task<Account?> Register(Account account)
        {
            account.Password = EncodePassword(account.Password);

            _dbContext.Add(account);
            await _dbContext.SaveChangesAsync();

            return await _accountRepository.GetAccountByEmailAsync(account.Email);
        }

        async public Task<Account?> Authenticate(string username, string password)
        {
            string encodedPassword = EncodePassword(password);

            var account = await ApplySpecification(new AccountByAuthData(username, encodedPassword))
                .FirstOrDefaultAsync();

            return account;
        }

        private string EncodePassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var encodedPassword = Convert.ToBase64String(bytes);
            return encodedPassword;
        }
    }
}
