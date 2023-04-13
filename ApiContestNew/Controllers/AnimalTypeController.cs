using ApiContestNew.Core.Interfaces.Services;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Dtos.AnimalType;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiContestNew.Controllers
{
    [Route("animals/types")]
    [ApiController]
    [Authorize]
    public class AnimalTypeController : ControllerBase
    {
        private readonly IAnimalTypeService _animalTypeService;
        private readonly IMapper _mapper;
        
        public AnimalTypeController(IAnimalTypeService animalTypeService, IMapper mapper)
        {
            _animalTypeService = animalTypeService;
            _mapper = mapper;
        }

        [HttpGet("{typeId}")]
        public async Task<ActionResult<GetAnimalTypeDto>> GetAnimalType(long typeId)
        {
            var response = await _animalTypeService.GetAnimalTypeAsync(typeId);

            return response.StatusCode switch
            {
                HttpStatusCode.OK => Ok(_mapper.Map<GetAnimalTypeDto>(response.Data)),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.NotFound => NotFound(),
                _ => StatusCode(500),
            };
        }

        [HttpPost]
        public async Task<ActionResult<GetAnimalTypeDto>> AddAnimalType(AddAnimalTypeDto dto)
        {
            var response = await _animalTypeService.AddAnimalTypeAsync(_mapper.Map<AnimalType>(dto));

            return response.StatusCode switch
            {
                HttpStatusCode.Created => StatusCode(201, _mapper.Map<GetAnimalTypeDto>(response.Data)),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.Conflict => Conflict(),
                _ => StatusCode(500),
            };
        }

        [HttpPut("{typeId}")]
        public async Task<ActionResult<GetAnimalTypeDto>> UpdateAnimalType(long typeId, UpdateAnimalTypeDto dto)
        {
            var response = await _animalTypeService.UpdateAnimalTypeAsync(typeId, _mapper.Map<AnimalType>(dto));

            return response.StatusCode switch
            {
                HttpStatusCode.OK => Ok(_mapper.Map<GetAnimalTypeDto>(response.Data)),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.NotFound => NotFound(),
                HttpStatusCode.Conflict => Conflict(),
                _ => StatusCode(500),
            };
        }

        [HttpDelete("{typeId}")]
        public async Task<ActionResult<GetAnimalTypeDto>> DeleteAnimalType(long typeId)
        {
            var response = await _animalTypeService.DeleteAnimalTypeAsync(typeId);

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
