using ApiContestNew.Core.Interfaces.Repositories;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Specifications.Animal;
using ApiContestNew.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiContestNew.Infrastructure.Repositories
{
    internal class AnimalRepository : BaseRepository<Animal>, IAnimalRepository
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

        public async Task<Animal?> AddVisitedLocationToAnimalAsync(Animal animal, AnimalVisitedLocation location)
        {
            animal.VisitedLocations.Add(location);
            await _dbContext.SaveChangesAsync();
            return await GetAnimalByIdAsync(animal.Id);
        }
    }
}
