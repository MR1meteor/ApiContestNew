using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Filters;

namespace ApiContestNew.Core.Interfaces.Repositories
{
    public interface IAnimalVisitedLocationRepository
    {
        Task<List<AnimalVisitedLocation>> GetAllAsync();
        Task<AnimalVisitedLocation?> GetLocationByIdAsync(long id);
        Task<List<AnimalVisitedLocation>> GetLocationsByAnimalAndFilterAsync(Animal animal, AnimalVisitedLocationFilter filter);
        Task<List<AnimalVisitedLocation>> GetLocationsByTimeAsync(DateTimeOffset? startTime, DateTimeOffset? endTime);
        Task<AnimalVisitedLocation?> AddLocationByPointAsync(LocationPoint point);
        Task<AnimalVisitedLocation?> UpdateLocationWithPointAsync(AnimalVisitedLocation location, LocationPoint point);
        Task<AnimalVisitedLocation?> DeleteLocationAsync(AnimalVisitedLocation location);
    }
}
