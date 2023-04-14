using ApiContestNew.Core.Interfaces.Services;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Filters;
using ApiContestNew.Dtos.Animal;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiContestNew.Controllers
{
    [Route("animals")]
    [ApiController]
    [Authorize]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalService _animalService;
        private readonly IAnimalTypeService _animalTypeService;
        private readonly IMapper _mapper;

        public AnimalController(
            IAnimalService animalService,
            IAnimalTypeService animalTypeService,
            IMapper mapper)
        {
            _animalService = animalService;
            _animalTypeService = animalTypeService;
            _mapper = mapper;
        }

        [HttpGet("{animalId}")]
        public async Task<ActionResult<GetAnimalDto>> GetAnimal(long animalId)
        {
            var response = await _animalService.GetAnimalAsync(animalId);

            return response.StatusCode switch
            {
                HttpStatusCode.OK => Ok(_mapper.Map<GetAnimalDto>(response.Data)),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.NotFound => NotFound(),
                _ => StatusCode(500),
            };
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<GetAnimalDto>>> GetAnimals([FromQuery] AnimalFilter filter)
        {
            var response = await _animalService.GetAnimalsAsync(filter);

            return response.StatusCode switch
            {
                HttpStatusCode.OK => Ok(_mapper.Map<List<GetAnimalDto>>(response.Data)),
                HttpStatusCode.BadRequest => BadRequest(),
                _ => StatusCode(500),
            };
        }

        [Authorize(Roles = "ADMIN, CHIPPER")]
        [HttpPost]
        public async Task<ActionResult<GetAnimalDto>> AddAnimal(AddAnimalDto dto)
        {
            var animal = _mapper.Map<Animal>(dto);

            if (dto.AnimalTypes.Length <= 0 ||  // TODO: remake that shit.. (+- 15 strings down)
                dto.AnimalTypes.Min() <= 0)
            {
                return BadRequest();
            }

            var types = (await _animalTypeService.GetAnimalTypesByIdsAsync(dto.AnimalTypes)).Data;

            if (types.Count < dto.AnimalTypes.Length)
            {
                return NotFound();
            }

            animal.AnimalTypes = types;

            var response = await _animalService.AddAnimalAsync(animal);

            return response.StatusCode switch
            {
                HttpStatusCode.Created => StatusCode(201, _mapper.Map<GetAnimalDto>(response.Data)),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.NotFound => NotFound(),
                HttpStatusCode.Conflict => Conflict(),
                _ => StatusCode(500),
            };
        }

        [Authorize(Roles = "ADMIN, CHIPPER")]
        [HttpPut("{animalId}")]
        public async Task<ActionResult<GetAnimalDto>> UpdateAnimal(long animalId, UpdateAnimalDto dto)
        {
            var response = await _animalService.UpdateAnimalAsync(animalId, _mapper.Map<Animal>(dto));

            return response.StatusCode switch
            {
                HttpStatusCode.OK => Ok(_mapper.Map<GetAnimalDto>(response.Data)),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.NotFound => NotFound(),
                _ => StatusCode(500),
            };
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{animalId}")]
        public async Task<ActionResult<GetAnimalDto>> DeleteAnimal(long animalId)
        {
            if (string.IsNullOrWhiteSpace(Request.Headers.Authorization))
            {
                return Unauthorized();
            }

            var response = await _animalService.DeleteAnimalAsync(animalId);

            return response.StatusCode switch
            {
                HttpStatusCode.OK => StatusCode(200),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.NotFound => NotFound(),
                _ => StatusCode(500),
            };
        }

        [Authorize(Roles = "ADMIN, CHIPPER")]
        [HttpPost("{animalId}/types/{typeId}")]
        public async Task<ActionResult<GetAnimalDto>> AddAnimalTypeToAnimal(long animalId, long typeId)
        {
            var response = await _animalService.AddAnimalTypeToAnimalAsync(animalId, typeId);

            return response.StatusCode switch
            {
                HttpStatusCode.Created => StatusCode(201, _mapper.Map<GetAnimalDto>(response.Data)),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.NotFound => NotFound(),
                HttpStatusCode.Conflict => Conflict(),
                _ => StatusCode(500),
            };
        }

        [Authorize(Roles = "ADMIN, CHIPPER")]
        [HttpPut("{animalId}/types")]
        public async Task<ActionResult<GetAnimalDto>> UpdateAnimalTypeAtAnimal(long animalId, UpdateAnimalTypeAtAnimalDto dto)
        {
            var response = await _animalService.UpdateAnimalTypeAtAnimalAsync(animalId, dto.OldTypeId, dto.NewTypeId);

            return response.StatusCode switch
            {
                HttpStatusCode.OK => Ok(_mapper.Map<GetAnimalDto>(response.Data)),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.NotFound => NotFound(),
                HttpStatusCode.Conflict => Conflict(),
                _ => StatusCode(500),
            };
        }

        [Authorize(Roles = "ADMIN, CHIPPER")]
        [HttpDelete("{animalId}/types/{typeId}")]
        public async Task<ActionResult<GetAnimalDto>> DeleteAnimalTypeAtAnimal(long animalId, long typeId)
        {
            var response = await _animalService.DeleteAnimalTypeAtAnimalAsync(animalId, typeId);

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
