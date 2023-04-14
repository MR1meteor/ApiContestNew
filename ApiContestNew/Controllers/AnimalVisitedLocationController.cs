using ApiContestNew.Core.Interfaces.Services;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Filters;
using ApiContestNew.Dtos.AnimalVisitedLocation;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiContestNew.Controllers
{
    [Route("animals/{animalId}/locations")]
    [ApiController]
    [Authorize]
    public class AnimalVisitedLocationController : ControllerBase
    {
        private readonly IAnimalVisitedLocationService _animalVisitedLocationService;
        private readonly IMapper _mapper;

        public AnimalVisitedLocationController(IAnimalVisitedLocationService animalVisitedLocationService, IMapper mapper)
        {
            _animalVisitedLocationService = animalVisitedLocationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetAnimalVisitedLocationDto>>> GetAnimalVisitedLocations(
            long animalId, [FromQuery] AnimalVisitedLocationFilter filter)
        {
            var response = await _animalVisitedLocationService.GetVisitedLocationsAsync(animalId, filter);

            return response.StatusCode switch
            {
                HttpStatusCode.OK => Ok(_mapper.Map<List<GetAnimalVisitedLocationDto>>(response.Data)),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.NotFound => NotFound(),
                _ => StatusCode(500),
            };
        }

        [Authorize(Roles = "ADMIN, CHIPPER")]
        [HttpPost("{pointId}")]
        public async Task<ActionResult<GetAnimalVisitedLocationDto>> AddAnimalVisitedLocation(long animalId, long pointId)
        {
            var response = await _animalVisitedLocationService.AddVisitedLocationAsync(animalId, pointId);

            return response.StatusCode switch
            {
                HttpStatusCode.Created => StatusCode(201, _mapper.Map<GetAnimalVisitedLocationDto>(response.Data)),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.NotFound => NotFound(),
                _ => StatusCode(500),
            };
        }

        [Authorize(Roles = "ADMIN, CHIPPER")]
        [HttpPut]
        public async Task<ActionResult<GetAnimalVisitedLocationDto>> UpdateAnimalVisitedLocation(long animalId, UpdateAnimalVisitedLocationDto dto)
        {
            var response = await _animalVisitedLocationService.UpdateVisitedLocationAsync(animalId, _mapper.Map<AnimalVisitedLocation>(dto));

            return response.StatusCode switch
            {
                HttpStatusCode.OK => Ok(_mapper.Map<GetAnimalVisitedLocationDto>(response.Data)),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.NotFound => NotFound(),
                _ => StatusCode(500),
            };
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{visitedPointId}")]
        public async Task<ActionResult<GetAnimalVisitedLocationDto>> DeleteAnimalVisitedLocation(long animalId, long visitedPointId)
        {
            var response = await _animalVisitedLocationService.DeleteVisitedLocationAsync(animalId, visitedPointId);

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
