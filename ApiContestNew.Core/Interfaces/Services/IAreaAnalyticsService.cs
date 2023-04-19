using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Filters;
using ApiContestNew.Core.Models.Responses;

namespace ApiContestNew.Core.Interfaces.Services
{
    public interface IAreaAnalyticsService
    {
        Task<ServiceResponse<AreaAnalytics?>> GetAnalyticsAsync(long areaId, AreaAnalyticsFilter filter);
    }
}
