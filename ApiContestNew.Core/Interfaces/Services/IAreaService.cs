using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Responses;

namespace ApiContestNew.Core.Interfaces.Services
{
    public interface IAreaService
    {
        Task<ServiceResponse<Area>> GetAreaAsync(long id);
        Task<ServiceResponse<Area>> AddAreaAsync(Area area);
    }
}
