using ApiContestNew.Core.Interfaces.Repositories;
using ApiContestNew.Core.Interfaces.Services;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Filters;
using ApiContestNew.Core.Models.Responses;

namespace ApiContestNew.Application.Services
{
    public class AreaAnalyticsService : IAreaAnalyticsService
    {
        private readonly IAreaRepository _areaRepository;
        private readonly IAnimalVisitedLocationRepository _visitedLocationRepository;

        public AreaAnalyticsService(
            IAreaRepository areaRepository,
            IAnimalVisitedLocationRepository visitedLocationRepository)
        {
            _areaRepository = areaRepository;
            _visitedLocationRepository = visitedLocationRepository;
        }

        async public Task<ServiceResponse<AreaAnalytics>> GetAnalyticsAsync(long areaId, AreaAnalyticsFilter filter)
        {
            if (areaId <= 0 || !filter.IsValid())
            {
                return new ServiceResponse400<AreaAnalytics>();
            }

            var area = await _areaRepository.GetAreaByIdAsync(areaId);

            if (area == null)
            {
                return new ServiceResponse404<AreaAnalytics>();
            }

            var visitedLocations = await _visitedLocationRepository.GetAllAsync();

            var locationsInsideArea = new List<AnimalVisitedLocation>();

            foreach (var location in visitedLocations)
            {
                if (LocationPoint.IsPointInsideArea(location.LocationPoint, (List<LocationPoint>)area.AreaPoints))
                {
                    locationsInsideArea.Add(location);
                }
                else
                {
                    for (int i = 0; i < area.AreaPoints.Count - 2; i++)
                    {
                        if (LocationPoint.IsPointOnLine(
                            location.LocationPoint,
                            ((List<LocationPoint>)area.AreaPoints)[i],
                            ((List<LocationPoint>)area.AreaPoints)[i + 1]))
                        {
                            locationsInsideArea.Add(location);
                        }
                    }
                }
            }

            List<Animal> animalsGone = new();
            List<Animal> animalsArrived = new();

            foreach (var location in locationsInsideArea)
            {
                var animal = location.Animal;
                var locationIndex = 0;
                for (locationIndex = 0; locationIndex < animal.AnimalTypes.Count; locationIndex++)
                {
                    if (((List<AnimalVisitedLocation>)animal.VisitedLocations)[locationIndex] == location)
                    {
                        break;
                    }
                }

                if (animal.VisitedLocations.Count > locationIndex + 1 &&
                    !LocationPoint.IsPointInsideArea(
                    ((List<AnimalVisitedLocation>)animal.VisitedLocations).ElementAt(locationIndex + 1).LocationPoint,
                    (List<LocationPoint>)area.AreaPoints) &&
                    (location.DateTimeOfVisitLocationPoint <= filter.EndDate || filter.EndDate == null) &&
                    !animalsGone.Contains(animal))
                {
                    animalsGone.Add(animal);
                }

                if (locationIndex >= 1 &&
                    !LocationPoint.IsPointInsideArea(
                    ((List<AnimalVisitedLocation>)animal.VisitedLocations).ElementAt(locationIndex - 1).LocationPoint,
                    (List<LocationPoint>)area.AreaPoints) &&
                    (location.DateTimeOfVisitLocationPoint >= filter.StartDate || filter.StartDate == null) &&
                    !animalsArrived.Contains(animal))
                {
                    animalsArrived.Add(animal);
                }
            }

            var analytics = new AreaAnalytics
            {
                TotalAnimalsArrived = animalsArrived.Count,
                TotalAnimalsGone = animalsGone.Count,
            };

            return new ServiceResponse200<AreaAnalytics>(data: analytics);
        }
    }
}
