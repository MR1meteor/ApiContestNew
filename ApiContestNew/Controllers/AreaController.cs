using ApiContestNew.Core.Interfaces.Services;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Dtos.AnimalVisitedLocation;
using ApiContestNew.Dtos.Area;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiContestNew.Controllers
{
    [Authorize]
    [Route("areas")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService _areaService;
        private readonly IMapper _mapper;

        public AreaController(IAreaService areaService, IMapper mapper)
        {
            _areaService = areaService;
            _mapper = mapper;
        }

        [HttpGet("{areaId}")]
        public async Task<ActionResult<GetAreaDto>> GetArea(long areaId)
        {
            var response = await _areaService.GetAreaAsync(areaId);

            return response.StatusCode switch
            {
                HttpStatusCode.OK => Ok(_mapper.Map<GetAreaDto>(response.Data)),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.NotFound => NotFound(),
                _ => StatusCode(500),
            };
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<ActionResult<GetAreaDto>> AddArea(AddAreaDto dto)
        {
            var response = await _areaService.AddAreaAsync(_mapper.Map<Area>(dto));

            return response.StatusCode switch
            {
                HttpStatusCode.Created => StatusCode(201, _mapper.Map<GetAreaDto>(response.Data)),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.Conflict => Conflict(),
                _ => StatusCode(500),
            };
        }
    }
}
