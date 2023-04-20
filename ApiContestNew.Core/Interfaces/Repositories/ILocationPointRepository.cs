using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Filters;

namespace ApiContestNew.Core.Interfaces.Repositories
{
    public interface ILocationPointRepository
    {
        Task<LocationPoint?> GetPointByIdAsync(long id);
        Task<LocationPoint?> GetPointByCoordsAsync(double latitude, double longitude);
        Task<LocationPoint?> GetPointByFilterAsync(LocationPointFilter filter);
        Task<LocationPoint?> AddPointAsync(LocationPoint point);
        Task<LocationPoint?> UpdatePointAsync(LocationPoint point);
        Task<LocationPoint?> DeletePointAsync(LocationPoint point);
    }
}
