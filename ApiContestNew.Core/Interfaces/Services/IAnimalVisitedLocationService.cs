using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Responses;

namespace ApiContestNew.Core.Interfaces.Services
{
    public interface IAnimalVisitedLocationService
    {
        //Task<ServiceResponse<List<AnimalVisitedLocation>>> GetVisitedLocationsAsync(long animalId, AnimalVisitedLocationFilter filter) TODO: Add filters aka specifications
        Task<ServiceResponse<AnimalVisitedLocation>> AddVisitedLocationAsync(long animalId, long pointId);
        Task<ServiceResponse<AnimalVisitedLocation>> UpdateVisitedLocationAsync(long animalId, AnimalVisitedLocation visitedLocation);
        Task<ServiceResponse<AnimalVisitedLocation>> DeleteVisitedLocationAsync(long animalId, long visitedPointId);
    }
}
