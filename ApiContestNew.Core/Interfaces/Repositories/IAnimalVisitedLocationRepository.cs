using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Filters;

namespace ApiContestNew.Core.Interfaces.Repositories
{
    public interface IAnimalVisitedLocationRepository
    {
        Task<List<AnimalVisitedLocation>> GetLocationsByAnimalAndFilterAsync(Animal animal, AnimalVisitedLocationFilter filter);
        Task<AnimalVisitedLocation?> AddLocationByPointAsync(LocationPoint point);
    }
}
