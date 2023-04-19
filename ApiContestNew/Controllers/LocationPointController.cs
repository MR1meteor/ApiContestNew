using ApiContestNew.Core.Interfaces.Services;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Dtos.LocationPoint;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace ApiContestNew.Controllers
{
    [Route("locations")]
    [ApiController]
    [Authorize]
    public class LocationPointController : ControllerBase
    {
        private readonly ILocationPointService _locationPointService;
        private readonly IMapper _mapper;

        public LocationPointController(ILocationPointService locationPointService, IMapper mapper)
        {
            _locationPointService = locationPointService;
            _mapper = mapper;

        }

        [HttpGet("{pointId}")]
        public async Task<ActionResult<GetLocationPointDto>> GetLocationPoint(long pointId)
        {
            var response = await _locationPointService.GetPointAsync(pointId);

            return response.StatusCode switch
            {
                HttpStatusCode.OK => Ok(_mapper.Map<GetLocationPointDto>(response.Data)),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.NotFound => NotFound(),
                _ => StatusCode(500),
            };
        }

        [Authorize(Roles = "ADMIN, CHIPPER")]
        [HttpPost]
        public async Task<ActionResult<GetLocationPointDto>> AddLocationPoint(AddLocationPointDto dto)
        {
            var response = await _locationPointService.AddPointAsync(_mapper.Map<LocationPoint>(dto));

            return response.StatusCode switch
            {
                HttpStatusCode.Created => StatusCode(201, _mapper.Map<GetLocationPointDto>(response.Data)),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.Conflict => Conflict(_mapper.Map<GetLocationPointDto>(response.Data)),
                _ => StatusCode(500),
            };
        }

        [Authorize(Roles = "ADMIN, CHIPPER")]
        [HttpPut("{pointId}")]
        public async Task<ActionResult<GetLocationPointDto>> UpdateLocationPoint(long pointId, UpdateLocationPointDto dto)
        {
            var response = await _locationPointService.UpdatePointAsync(pointId, _mapper.Map<LocationPoint>(dto));

            return response.StatusCode switch
            {
                HttpStatusCode.OK => Ok(_mapper.Map<GetLocationPointDto>(response.Data)),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.NotFound => NotFound(),
                HttpStatusCode.Conflict => Conflict(),
                _ => StatusCode(500),
            };
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{pointId}")]
        public async Task<ActionResult<GetLocationPointDto>> DeleteLocationPoint(long pointId)
        {
            var response = await _locationPointService.DeletePointAsync(pointId);

            return response.StatusCode switch
            {
                HttpStatusCode.OK => StatusCode(200),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.NotFound => NotFound(),
                _ => StatusCode(500),
            };
        }
    }
}
