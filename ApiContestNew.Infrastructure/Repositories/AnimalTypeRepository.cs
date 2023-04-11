using ApiContestNew.Core.Interfaces.Repositories;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Specifications.AnimalType;
using ApiContestNew.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiContestNew.Infrastructure.Repositories
{
    public class AnimalTypeRepository : BaseRepository<AnimalType>, IAnimalTypeRepository
    {

        public AnimalTypeRepository(DataContext dbContext)
            : base(dbContext)
        {
            
        }

        async public Task<AnimalType?> GetTypeByIdAsync(long id)
        {
            return await ApplySpecification(new TypeByIdWithAnimals(id))
                .FirstOrDefaultAsync();
        }

        async public Task<AnimalType?> GetTypeByTypeAsync(string type)
        {
            return await ApplySpecification(new TypeByTypeWithAnimals(type))
                .FirstOrDefaultAsync();
        }

        async public Task<List<AnimalType>> GetTypesByIdsAsync(long[] ids)
        {
            return await ApplySpecification(new TypeByIds(ids))
                .ToListAsync();
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
