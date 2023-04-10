using ApiContestNew.Core.Interfaces.Repositories;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Filters;
using ApiContestNew.Core.Specifications.AnimalVisitedLocation;
using ApiContestNew.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiContestNew.Infrastructure.Repositories
{
    public class AnimalVisitedLocationRepository : BaseRepository<AnimalVisitedLocation>, IAnimalVisitedLocationRepository
    {
        public AnimalVisitedLocationRepository(DataContext dataContext)
            : base(dataContext)
        {

        }

        public async Task<AnimalVisitedLocation?> GetLocationByIdAsync(long id)
        {
            return await ApplySpecification(new LocationByIdWithAll(id))
                .FirstOrDefaultAsync();
        }
        
        public async Task<List<AnimalVisitedLocation>> GetLocationsByAnimalAndFilterAsync(Animal animal, AnimalVisitedLocationFilter filter)
        {
            return await ApplySpecification(new LocationByAnimalAndFilterWithAll(animal, filter))
                .ToListAsync();
        }

        public async Task<AnimalVisitedLocation?> AddLocationByPointAsync(LocationPoint point)
        {
            var location = new AnimalVisitedLocation();
            location.LocationPoint = point;

            _dbContext.AnimalVisitedLocations.Add(location);
            await _dbContext.SaveChangesAsync();

            return await GetLocationByIdAsync(location.Id);
        }

        public async Task<AnimalVisitedLocation?> UpdateLocationWithPointAsync(AnimalVisitedLocation location, LocationPoint point)
        {
            location.LocationPoint = point;
            await _dbContext.SaveChangesAsync();

            return await GetLocationByIdAsync(location.Id);
        }

        public async Task<AnimalVisitedLocation?> DeleteLocationAsync(AnimalVisitedLocation location)
        {
            _dbContext.Remove(location);
            await _dbContext.SaveChangesAsync();
            return null;
        }
    }
}
