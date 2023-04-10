using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Responses;

namespace ApiContestNew.Core.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse<Account>> Register(Account account);
    }
}
