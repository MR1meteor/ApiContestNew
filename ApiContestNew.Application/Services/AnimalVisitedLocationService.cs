using ApiContestNew.Core.Interfaces.Repositories;
using ApiContestNew.Core.Interfaces.Services;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Filters;
using ApiContestNew.Core.Models.Responses;

namespace ApiContestNew.Application.Services
{
    public class AnimalVisitedLocationService : IAnimalVisitedLocationService
    {
        private readonly IAnimalVisitedLocationRepository _visitedLocationRepository;
        private readonly IAnimalRepository _animalRepository;
        private readonly ILocationPointRepository _locationPointRepository;

        public AnimalVisitedLocationService(
            IAnimalVisitedLocationRepository visitedLocationRepository,
            IAnimalRepository animalRepository,
            ILocationPointRepository locationPointRepository)
        {
            _visitedLocationRepository = visitedLocationRepository;
            _animalRepository = animalRepository;
            _locationPointRepository = locationPointRepository;
        }

        async public Task<ServiceResponse<List<AnimalVisitedLocation>>> GetVisitedLocationsAsync(long animalId, AnimalVisitedLocationFilter filter)
        {
            if (animalId <= 0 || !filter.IsValid())
            {
                return new ServiceResponse400<List<AnimalVisitedLocation>>();
            }

            var animal = await _animalRepository.GetAnimalByIdAsync(animalId);
            if (animal == null)
            {
                return new ServiceResponse404<List<AnimalVisitedLocation>>();
            }

            var locations = await _visitedLocationRepository.GetLocationsByAnimalAndFilterAsync(animal, filter);
            return new ServiceResponse200<List<AnimalVisitedLocation>>(data: locations);
        }

        async public Task<ServiceResponse<AnimalVisitedLocation>> AddVisitedLocationAsync(long animalId, long pointId)
        {
            if (animalId <= 0 || pointId <= 0)
            {
                return new ServiceResponse400<AnimalVisitedLocation>();
            }

            var animal = await _animalRepository.GetAnimalByIdAsync(animalId);
            var point = await _locationPointRepository.GetPointByIdAsync(pointId);

            if(animal == null || point == null)
            {
                return new ServiceResponse404<AnimalVisitedLocation>();
            }

            if (!animal.IsAbleToAddVisitedLocation(point))
            {
                return new ServiceResponse400<AnimalVisitedLocation>();
            }

            var newLocation = await _visitedLocationRepository.AddLocationByPointAsync(point);
            await _animalRepository.AddVisitedLocationToAnimalAsync(animal, newLocation);

            return new ServiceResponse201<AnimalVisitedLocation>(data: newLocation); // TODO: Implement repository methods
        }

        async public Task<ServiceResponse<AnimalVisitedLocation>> UpdateVisitedLocationAsync(long animalId, AnimalVisitedLocation visitedLocation)
        {
            throw new NotImplementedException();
        }

        async public Task<ServiceResponse<AnimalVisitedLocation>> DeleteVisitedLocationAsync(long animalId, long visitedPointId)
        {
            throw new NotImplementedException();
        }
    }
}
