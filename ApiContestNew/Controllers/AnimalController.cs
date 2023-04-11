using ApiContestNew.Core.Interfaces.Services;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Filters;
using ApiContestNew.Dtos.Animal;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiContestNew.Controllers
{
    [Route("animal")]
    [ApiController]
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

        [HttpPost]
        public async Task<ActionResult<GetAnimalDto>> AddAnimal(AddAnimalDto dto)
        {
            var animal = _mapper.Map<Animal>(dto);

            var types = (await _animalTypeService.GetAnimalTypesByIdsAsync(dto.AnimalTypes)).Data;
            if (types == null)
            {
                return BadRequest();
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

        [HttpDelete("{animalId}")]
        public async Task<ActionResult<GetAnimalDto>> DeleteAnimal(long animalId)
        {
            var response = await _animalService.DeleteAnimalAsync(animalId);

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
