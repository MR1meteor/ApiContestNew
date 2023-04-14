using ApiContestNew.Core.Models.Entities;

namespace ApiContestNew.Core.Interfaces.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<Account?> Register(Account account);
        Task<Account?> Authenticate(string username, string password);
    }
}
