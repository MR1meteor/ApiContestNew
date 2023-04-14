using ApiContestNew.Core.Models.Entities;

namespace ApiContestNew.Core.Interfaces.Repositories
{
    public interface IAreaRepository
    {
        Task<Area?> GetAreaByIdAsync(long id);
    }
}
