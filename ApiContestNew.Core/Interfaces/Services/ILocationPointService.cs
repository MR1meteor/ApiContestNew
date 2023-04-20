using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Filters;
using ApiContestNew.Core.Models.Responses;

namespace ApiContestNew.Core.Interfaces.Services
{
    public interface ILocationPointService
    {
        Task<ServiceResponse<LocationPoint>> GetPointAsync(long id);
        Task<ServiceResponse<long>> GetPointIdByFilterAsync(LocationPointFilter filter);
        Task<ServiceResponse<string>> GetGeohashByFilterAsync(LocationPointFilter filter, int precision);
        Task<ServiceResponse<string>> GetEncodedGeohashByFilterAsync(LocationPointFilter filter, int precision);
        Task<ServiceResponse<LocationPoint>> AddPointAsync(LocationPoint point);
        Task<ServiceResponse<LocationPoint>> UpdatePointAsync(long id, LocationPoint point);
        Task<ServiceResponse<LocationPoint>> DeletePointAsync(long id);
    }
}
