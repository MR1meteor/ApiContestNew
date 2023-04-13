using ApiContestNew.Core.Interfaces.Repositories;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Filters;
using ApiContestNew.Core.Specifications.Animal;
using ApiContestNew.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiContestNew.Infrastructure.Repositories
{
    public class AnimalRepository : BaseRepository<Animal>, IAnimalRepository
    {
        public AnimalRepository(DataContext dataContext)
            : base(dataContext)
        {

        }

        public async Task<Animal?> GetAnimalByIdAsync(long id)
        {
            return await ApplySpecification(new AnimalByIdWithAll(id))
                .FirstOrDefaultAsync();
        }

        public async Task<List<Animal>> GetAnimalsByFilterAsync(AnimalFilter filter)
        {
            return await ApplySpecification(new AnimalByFilterWithAll(filter))
                .ToListAsync();
        }

        public async Task<Animal?> AddAnimalAsync(Animal animal)
        {
            animal.LifeStatus = "ALIVE";

            DateTimeOffset dateTime = DateTimeOffset.UtcNow;
            dateTime = dateTime.AddTicks(-(dateTime.Ticks % 10000));
            animal.ChippingDateTime = dateTime;

            _dbContext.Animals.Add(animal);
            await _dbContext.SaveChangesAsync();

            return await GetAnimalByIdAsync(animal.Id);
        }

        public async Task<Animal?> UpdateAnimalAsync(Animal animal)
        {
            var editableAnimal = await GetAnimalByIdAsync(animal.Id);
            editableAnimal.Weight = animal.Weight;
            editableAnimal.Length = animal.Length;
            editableAnimal.Height = animal.Height;
            editableAnimal.Gender = animal.Gender;
            editableAnimal.LifeStatus = animal.LifeStatus;
            if (animal.LifeStatus == "DEAD")
            {
                DateTimeOffset dateTime = DateTimeOffset.UtcNow;
                dateTime = dateTime.AddTicks(-(dateTime.Ticks % 10000));
                editableAnimal.DeathDateTime = dateTime;
            }
            editableAnimal.ChipperId = animal.ChipperId;
            editableAnimal.Chipper = animal.Chipper;
            editableAnimal.ChippingLocationId = animal.ChippingLocationId;
            editableAnimal.ChippingLocation = animal.ChippingLocation;

            await _dbContext.SaveChangesAsync();

            return await GetAnimalByIdAsync(editableAnimal.Id);
        }

        public async Task<Animal?> DeleteAnimalAsync(Animal animal)
        {
            _dbContext.Animals.Remove(animal);
            await _dbContext.SaveChangesAsync();

            return null;
        }

        public async Task<Animal?> AddVisitedLocationToAnimalAsync(Animal animal, AnimalVisitedLocation location)
        {
            animal.VisitedLocations.Add(location);
            await _dbContext.SaveChangesAsync();
            return await GetAnimalByIdAsync(animal.Id);
        }

        public async Task<Animal?> AddAnimalTypeToAnimalAsync(Animal animal, AnimalType type)
        {
            animal.AnimalTypes.Add(type);
            await _dbContext.SaveChangesAsync();
            return await GetAnimalByIdAsync(animal.Id);
        }

        public async Task<Animal?> UpdateAnimalTypeAtAnimalAsync(Animal animal, AnimalType oldType, AnimalType newType)
        {
            animal.AnimalTypes.Remove(oldType);
            animal.AnimalTypes.Add(newType);
            await _dbContext.SaveChangesAsync();
            return await GetAnimalByIdAsync(animal.Id);
        }

        public async Task<Animal?> DeleteAnimalTypeAtAnimalAsync(Animal animal, AnimalType type)
        {
            animal.AnimalTypes.Remove(type);
            await _dbContext.SaveChangesAsync();
            return await GetAnimalByIdAsync(animal.Id);
        }
    }
}
