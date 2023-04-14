using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Filters;

namespace ApiContestNew.Core.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task<Account?> GetAccountByIdAsync(int id);
        Task<Account?> GetAccountByEmailAsync(string email);
        Task<List<Account>> GetAccountsByFilterAsync(AccountFilter filter);
        Task<Account?> AddAccountAsync(Account account);
        Task<Account?> UpdateAccountAsync(Account account);
        Task<Account?> DeleteAccountAsync(Account account);
    }
}
