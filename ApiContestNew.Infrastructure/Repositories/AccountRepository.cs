using ApiContestNew.Core.Interfaces.Repositories;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Filters;
using ApiContestNew.Core.Specifications.Account;
using ApiContestNew.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace ApiContestNew.Infrastructure.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(DataContext dbContext)
            : base(dbContext)
        {

        }

        async public Task<Account?> GetAccountByIdAsync(int id)
        {
            return await ApplySpecification(new AccountByIdWithAnimals(id))
                .FirstOrDefaultAsync();
        }

        async public Task<Account?> GetAccountByEmailAsync(string email)
        {
            return await ApplySpecification(new AccountByEmailWithAnimals(email))
                .FirstOrDefaultAsync();
        }

        async public Task<List<Account>> GetAccountsByFilterAsync(AccountFilter filter)
        {
            return await ApplySpecification(new AccountByFilterWithAnimals(filter))
                .ToListAsync();
        }

        async public Task<Account?> UpdateAccountAsync(Account account)
        {
            var editableAccount = await GetAccountByIdAsync(account.Id);
            #pragma warning disable CS8602 // Dereference of a possibly null reference.
            editableAccount.FirstName = account.FirstName;
            editableAccount.LastName = account.LastName;
            editableAccount.Email = account.Email;
            editableAccount.Password = EncodePassword(account.Password);
            #pragma warning restore CS8602 // Dereference of a possibly null reference.
            await _dbContext.SaveChangesAsync();
            return await GetAccountByIdAsync(editableAccount.Id);
        }

        async public Task<Account?> DeleteAccountAsync(Account account)
        {
            _dbContext.Accounts.Remove(account);
            await _dbContext.SaveChangesAsync();
            return null;
        }

        private string EncodePassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var encodedPassword = Convert.ToBase64String(bytes);
            return encodedPassword;
        }
    }
}
