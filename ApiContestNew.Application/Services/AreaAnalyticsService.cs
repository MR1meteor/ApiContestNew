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
        private readonly IAnimalRepository _animalRepository;

        public AreaAnalyticsService(
            IAreaRepository areaRepository,
            IAnimalVisitedLocationRepository visitedLocationRepository,
            IAnimalRepository animalRepository)
        {
            _areaRepository = areaRepository;
            _visitedLocationRepository = visitedLocationRepository;
            _animalRepository = animalRepository;
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

            var animals = await _animalRepository.GetAllAsync();

            List<Animal> animalsGone = new();
            List<Animal> animalsArrived = new();
            List<Animal> animalsStayed = new();
            List<AnimalAnalytics> animalAnalytics = new();

            foreach (var animal in animals)
            {
                bool gone = false;
                bool arrived = false;
                bool stayed = false;
                bool hasPointsInArea = false;

                if (!hasPointsInArea &&
                    (LocationPoint.IsPointInsideArea(animal.ChippingLocation, (List<LocationPoint>)area.AreaPoints) ||
                    LocationPoint.IsPointOnArea(animal.ChippingLocation, area)))
                {
                    hasPointsInArea = true;
                }

                if (animal.VisitedLocations.Count > 0 &&
                    (LocationPoint.IsPointInsideArea(animal.ChippingLocation, (List<LocationPoint>)area.AreaPoints) ||
                    LocationPoint.IsPointOnArea(animal.ChippingLocation, area)) &&
                    !LocationPoint.IsPointInsideArea(
                        ((List<AnimalVisitedLocation>)animal.VisitedLocations)[0].LocationPoint,(List<LocationPoint>)area.AreaPoints) &&
                    !LocationPoint.IsPointOnArea(((List<AnimalVisitedLocation>)animal.VisitedLocations)[0].LocationPoint, area))
                {
                    gone = true;
                    animalsGone.Add(animal);
                }

                if (animal.VisitedLocations.Count > 0 &&
                    !LocationPoint.IsPointInsideArea(animal.ChippingLocation, (List<LocationPoint>)area.AreaPoints) &&
                    !LocationPoint.IsPointOnArea(animal.ChippingLocation, area) &&
                    (LocationPoint.IsPointInsideArea(
                        ((List<AnimalVisitedLocation>)animal.VisitedLocations)[0].LocationPoint, (List<LocationPoint>)area.AreaPoints) ||
                    LocationPoint.IsPointOnArea(((List<AnimalVisitedLocation>)animal.VisitedLocations)[0].LocationPoint, area)))
                {
                    arrived = true;
                    animalsArrived.Add(animal);
                }

                for (int i = 0; i < animal.VisitedLocations.Count; i++)
                {
                    var location = ((List<AnimalVisitedLocation>)animal.VisitedLocations)[i];

                    if (!hasPointsInArea &&
                        (LocationPoint.IsPointInsideArea(location.LocationPoint, (List<LocationPoint>)area.AreaPoints) ||
                        LocationPoint.IsPointOnArea(location.LocationPoint, area)))
                    {
                        hasPointsInArea = true;
                    }

                    if (
                        !arrived &&
                        (LocationPoint.IsPointInsideArea(location.LocationPoint, (List<LocationPoint>)area.AreaPoints) ||
                        LocationPoint.IsPointOnArea(location.LocationPoint, area)) &&
                        i >= 1 &&
                        !LocationPoint.IsPointInsideArea(
                        ((List<AnimalVisitedLocation>)animal.VisitedLocations).ElementAt(i - 1).LocationPoint,
                        (List<LocationPoint>)area.AreaPoints) &&
                        (location.DateTimeOfVisitLocationPoint >= filter.StartDate || filter.StartDate == null) &&
                        !animalsArrived.Contains(animal)
                        )
                    {
                        arrived = true;
                        animalsArrived.Add(animal);
                    }

                    if (
                        !gone &&
                        (LocationPoint.IsPointInsideArea(location.LocationPoint, (List<LocationPoint>)area.AreaPoints) ||
                        LocationPoint.IsPointOnArea(location.LocationPoint, area)) &&
                        animal.VisitedLocations.Count > i + 1 &&
                        !LocationPoint.IsPointInsideArea(
                        ((List<AnimalVisitedLocation>)animal.VisitedLocations).ElementAt(i + 1).LocationPoint,
                        (List<LocationPoint>)area.AreaPoints) &&
                        (location.DateTimeOfVisitLocationPoint <= filter.EndDate || filter.EndDate == null) &&
                        !animalsGone.Contains(animal)
                       )
                    {
                        gone = true;
                        animalsGone.Add(animal);
                    }

                    if (gone && arrived)
                    {
                        break;
                    }
                }

                if (!gone && !arrived && hasPointsInArea ||
                    !gone && arrived && hasPointsInArea)
                {
                    stayed = true;
                    animalsStayed.Add(animal);
                }

                if (arrived || stayed || gone)
                {
                    foreach (var type in animal.AnimalTypes)
                    {
                        var localAnalytics = animalAnalytics.FirstOrDefault(a => a.AnimalTypeId == type.Id);

                        if (localAnalytics == null)
                        {
                            animalAnalytics.Add(new AnimalAnalytics
                            {
                                AnimalType = type.Type,
                                AnimalTypeId = type.Id,
                                AnimalsArrived = 0,
                                AnimalsGone = 0,
                                QuantityAnimals = 0
                            });
                        }

                        if (arrived)
                        {
                            animalAnalytics.FirstOrDefault(a => a.AnimalTypeId == type.Id).AnimalsArrived++;
                        }

                        if (stayed)
                        {
                            animalAnalytics.FirstOrDefault(a => a.AnimalTypeId == type.Id).QuantityAnimals++;
                        }

                        if (gone)
                        {
                            animalAnalytics.FirstOrDefault(a => a.AnimalTypeId == type.Id).AnimalsGone++;
                        }
                    }
                }
            }

            //var visitedLocations = await _visitedLocationRepository.GetAllAsync();

            //var locationsInsideArea = new List<AnimalVisitedLocation>();

            //foreach (var location in visitedLocations)
            //{
            //    if (LocationPoint.IsPointInsideArea(location.LocationPoint, (List<LocationPoint>)area.AreaPoints))
            //    {
            //        locationsInsideArea.Add(location);
            //    }
            //    else
            //    {
            //        for (int i = 0; i < area.AreaPoints.Count - 2; i++)
            //        {
            //            if (LocationPoint.IsPointOnLine(
            //                location.LocationPoint,
            //                ((List<LocationPoint>)area.AreaPoints)[i],
            //                ((List<LocationPoint>)area.AreaPoints)[i + 1]))
            //            {
            //                locationsInsideArea.Add(location);
            //            }
            //        }
            //    }
            //}

            

            //foreach (var location in locationsInsideArea)
            //{
            //    var animal = location.Animal;
            //    var locationIndex = 0;
            //    for (locationIndex = 0; locationIndex < animal.AnimalTypes.Count; locationIndex++)
            //    {
            //        if (((List<AnimalVisitedLocation>)animal.VisitedLocations)[locationIndex] == location)
            //        {
            //            break;
            //        }
            //    }

            //    if (animal.VisitedLocations.Count > locationIndex + 1 &&
            //        !LocationPoint.IsPointInsideArea(
            //        ((List<AnimalVisitedLocation>)animal.VisitedLocations).ElementAt(locationIndex + 1).LocationPoint,
            //        (List<LocationPoint>)area.AreaPoints) &&
            //        (location.DateTimeOfVisitLocationPoint <= filter.EndDate || filter.EndDate == null) &&
            //        !animalsGone.Contains(animal))
            //    {
            //        animalsGone.Add(animal);
            //    }

            //    if (locationIndex >= 1 &&
            //        !LocationPoint.IsPointInsideArea(
            //        ((List<AnimalVisitedLocation>)animal.VisitedLocations).ElementAt(locationIndex - 1).LocationPoint,
            //        (List<LocationPoint>)area.AreaPoints) &&
            //        (location.DateTimeOfVisitLocationPoint >= filter.StartDate || filter.StartDate == null) &&
            //        !animalsArrived.Contains(animal))
            //    {
            //        animalsArrived.Add(animal);
            //    }
            //}

            var analytics = new AreaAnalytics
            {
                TotalAnimalsArrived = animalsArrived.Count,
                TotalAnimalsGone = animalsGone.Count,
                TotalQuantityAnimals = animalsStayed.Count,
                AnimalsAnalytics = animalAnalytics
            };

            return new ServiceResponse200<AreaAnalytics>(data: analytics);
        }
    }
}
