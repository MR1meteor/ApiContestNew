﻿using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Filters;

namespace ApiContestNew.Core.Interfaces.Repositories
{
    public interface IAnimalRepository
    {
        Task<List<Animal>> GetAllAsync();
        Task<Animal?> GetAnimalByIdAsync(long id);
        Task<List<Animal>> GetAnimalsByFilterAsync(AnimalFilter filter);
        Task<Animal?> AddAnimalAsync(Animal animal);
        Task<Animal?> UpdateAnimalAsync(Animal animal);
        Task<Animal?> DeleteAnimalAsync(Animal animal);
        Task<Animal?> AddVisitedLocationToAnimalAsync(Animal animal, AnimalVisitedLocation location);
        Task<Animal?> AddAnimalTypeToAnimalAsync(Animal animal, AnimalType type);
        Task<Animal?> UpdateAnimalTypeAtAnimalAsync(Animal animal, AnimalType oldType, AnimalType newType);
        Task<Animal?> DeleteAnimalTypeAtAnimalAsync(Animal animal, AnimalType type);
    }
}
