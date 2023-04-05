using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Responses;

namespace ApiContestNew.Core.Interfaces.Services
{
    public interface ILocationPointService
    {
        Task<ServiceResponse<LocationPoint>> GetPointAsync(long id);
        Task<ServiceResponse<LocationPoint>> AddPointAsync(LocationPoint point);
        Task<ServiceResponse<LocationPoint>> UpdatePointAsync(long id, LocationPoint point);
        Task<ServiceResponse<LocationPoint>> DeletePointAsync(long id);
    }
}
