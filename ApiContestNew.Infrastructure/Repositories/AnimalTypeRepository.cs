using ApiContestNew.Core.Interfaces.Repositories;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiContestNew.Infrastructure.Repositories
{
    public class AnimalTypeRepository : IAnimalTypeRepository
    {
        private DataContext _dbContext;

        public AnimalTypeRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        async public Task<AnimalType?> GetTypeByIdAsync(long id)
        {
            return await _dbContext.AnimalTypes
                .Include(t => t.Animals)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        async public Task<AnimalType?> GetTypeByTypeAsync(string type)
        {
            return await _dbContext.AnimalTypes
                .Include(t => t.Animals)
                .FirstOrDefaultAsync(t => t.Type == type);
        }

        async public Task<AnimalType?> AddTypeAsync(AnimalType animalType)
        {
            _dbContext.AnimalTypes.Add(animalType);
            await _dbContext.SaveChangesAsync();
            return await GetTypeByIdAsync(animalType.Id);
        }

        async public Task<AnimalType?> UpdateTypeAsync(AnimalType animalType)
        {
            var editableType = await GetTypeByIdAsync(animalType.Id);
            #pragma warning disable CS8602 // Dereference of a possibly null reference.
            editableType.Type = animalType.Type;
            #pragma warning restore CS8602 // Dereference of a possibly null reference.
            await _dbContext.SaveChangesAsync();
            return await GetTypeByIdAsync(editableType.Id);
        }

        async public Task<AnimalType?> DeleteTypeAsync(AnimalType animalType)
        {
            _dbContext.Remove(animalType);
            await _dbContext.SaveChangesAsync();
            return null;
        }
    }
}
