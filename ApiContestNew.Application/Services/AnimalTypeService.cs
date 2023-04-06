﻿using ApiContestNew.Core.Interfaces.Repositories;
using ApiContestNew.Core.Interfaces.Services;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Responses;

namespace ApiContestNew.Application.Services
{
    public class AnimalTypeService : IAnimalTypeService
    {
        private IAnimalTypeRepository _animalTypeRepo;

        public AnimalTypeService(IAnimalTypeRepository animalTypeRepo)
        {
            _animalTypeRepo = animalTypeRepo;
        }

        async public Task<ServiceResponse<AnimalType>> GetAnimalTypeAsync(long id)
        {
            if (id <= 0)
            {
                return new ServiceResponse400<AnimalType>();
            }

            var type = await _animalTypeRepo.GetTypeByIdAsync(id);
            if (type == null)
            {
                return new ServiceResponse404<AnimalType>();
            }

            return new ServiceResponse200<AnimalType>(data: type);
        }
        
        async public Task<ServiceResponse<AnimalType>> AddAnimalTypeAsync(AnimalType animalType)
        {
            if (!animalType.IsValidWithoutId())
            {
                return new ServiceResponse400<AnimalType>();
            }

            var type = await _animalTypeRepo.GetTypeByTypeAsync(animalType.Type);

            if (type != null) 
            {
                return new ServiceResponse409<AnimalType>();
            }

            var createdType = await _animalTypeRepo.AddTypeAsync(animalType);

            return new ServiceResponse201<AnimalType>(data: createdType);
        }

        async public Task<ServiceResponse<AnimalType>> UpdateAnimalTypeAsync(long id, AnimalType animalType)
        {
            animalType.Id = id;
            if (!animalType.IsValid()) 
            {
                return new ServiceResponse400<AnimalType>();
            }

            var type = await _animalTypeRepo.GetTypeByIdAsync(id);
            if (type == null)
            {
                return new ServiceResponse404<AnimalType>();
            }

            var equalType = await _animalTypeRepo.GetTypeByTypeAsync(animalType.Type);
            if (equalType != null)
            {
                return new ServiceResponse409<AnimalType>();
            }

            var editedType = await _animalTypeRepo.UpdateTypeAsync(animalType);

            return new ServiceResponse200<AnimalType>(data: editedType);
        }
        
        async public Task<ServiceResponse<AnimalType>> DeleteAnimalTypeAsync(long id)
        {
            if (id <= 0)
            {
                return new ServiceResponse400<AnimalType>();
            }

            var type = await _animalTypeRepo.GetTypeByIdAsync(id);
            if (type == null)
            {
                return new ServiceResponse404<AnimalType>();
            }

            if (type.Animals.Count > 0)
            {
                return new ServiceResponse400<AnimalType>();
            }

            await _animalTypeRepo.DeleteTypeAsync(type);

            return new ServiceResponse200<AnimalType>();
        }
    }
}
