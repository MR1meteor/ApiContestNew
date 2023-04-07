using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Filters;
using ApiContestNew.Core.Models.Responses;

namespace ApiContestNew.Core.Interfaces.Services
{
    public interface IAccountService
    {
        Task<ServiceResponse<Account>> GetAccountAsync(int id);
        Task<ServiceResponse<List<Account>>> GetAccountsAsync(AccountFilter accountFilter);
        Task<ServiceResponse<Account>> UpdateAccountAsync(int id, Account account);
        Task<ServiceResponse<Account>> DeleteAccountAsync(int id);
    }
}
