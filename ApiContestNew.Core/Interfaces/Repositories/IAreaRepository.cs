using ApiContestNew.Core.Models.Entities;

namespace ApiContestNew.Core.Interfaces.Repositories
{
    public interface IAreaRepository
    {
        Task<Area?> GetAreaByIdAsync(long id);
        Task<Area?> GetAreaByNameAsync(string name);
        Task<Area?> AddAreaAsync(Area area);
        Task<Area?> DeleteAreaAsync(Area area);
    }
}
