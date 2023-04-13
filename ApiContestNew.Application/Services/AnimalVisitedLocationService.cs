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

            return new ServiceResponse201<AnimalVisitedLocation>(data: newLocation);
        }

        async public Task<ServiceResponse<AnimalVisitedLocation>> UpdateVisitedLocationAsync(long animalId, AnimalVisitedLocation visitedLocation)
        {
            if (animalId <= 0 || !visitedLocation.IsValid())
            {
                return new ServiceResponse400<AnimalVisitedLocation>();
            }

            var point = await _locationPointRepository.GetPointByIdAsync(visitedLocation.LocationPointId);
            var location = await _visitedLocationRepository.GetLocationByIdAsync(visitedLocation.Id);
            var animal = await _animalRepository.GetAnimalByIdAsync(animalId);

            if (point == null || location == null || location == null ||
                !animal.VisitedLocations.Contains(location))
            {
                return new ServiceResponse404<AnimalVisitedLocation>();
            }

            animal.VisitedLocations = animal.VisitedLocations.OrderBy(l => l.Id).ToList();
            if (!animal.IsAbleToUpdateVisitedLocation(location, point))
            {
                return new ServiceResponse400<AnimalVisitedLocation>();
            }

            var newLocation = await _visitedLocationRepository.UpdateLocationWithPointAsync(location, point);

            return new ServiceResponse200<AnimalVisitedLocation>(data: newLocation);
        }

        async public Task<ServiceResponse<AnimalVisitedLocation>> DeleteVisitedLocationAsync(long animalId, long visitedPointId)
        {
            if (animalId <= 0 || visitedPointId <= 0)
            {
                return new ServiceResponse400<AnimalVisitedLocation>();
            }

            var animal = await _animalRepository.GetAnimalByIdAsync(animalId);
            var location = await _visitedLocationRepository.GetLocationByIdAsync(visitedPointId);

            if (animal == null || location == null || !animal.VisitedLocations.Contains(location))
            {
                return new ServiceResponse404<AnimalVisitedLocation>();
            }


            int nextLocationIndex = animal.NextLocationIndexIfEqual(location);

            if (nextLocationIndex != -1)
            {
                await _visitedLocationRepository.DeleteLocationAsync(animal.VisitedLocations.ElementAt(nextLocationIndex));
            }

            await _visitedLocationRepository.DeleteLocationAsync(location);

            return new ServiceResponse200<AnimalVisitedLocation>();
        }
    }
}
