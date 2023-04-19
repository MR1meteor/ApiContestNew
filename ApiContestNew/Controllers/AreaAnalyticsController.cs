using ApiContestNew.Core.Interfaces.Services;
using ApiContestNew.Core.Models.Filters;
using ApiContestNew.Dtos.Account;
using ApiContestNew.Dtos.Analytics;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiContestNew.Controllers
{
    [Authorize]
    [Route("")]
    [ApiController]
    public class AreaAnalyticsController : ControllerBase
    {
        IAreaAnalyticsService _areaAnalyticsService;
        IMapper _mapper;

        public AreaAnalyticsController(IAreaAnalyticsService areaAnalyticsService, IMapper mapper)
        {
            _areaAnalyticsService = areaAnalyticsService;
            _mapper = mapper;
        }

        [HttpGet("areas/{areaId}/analytics")]
        public async Task<ActionResult<GetAreaAnalyticsDto>> GetAreaAnalytics(long areaId, [FromQuery]AreaAnalyticsFilter filter)
        {
            var response = await _areaAnalyticsService.GetAnalyticsAsync(areaId, filter);

            return response.StatusCode switch
            {
                HttpStatusCode.OK => Ok(_mapper.Map<GetAreaAnalyticsDto>(response.Data)),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.NotFound => NotFound(),
                _ => StatusCode(500),
            };
        }
    }
}
