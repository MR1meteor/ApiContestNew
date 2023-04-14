using ApiContestNew.Core.Interfaces.Services;
using ApiContestNew.Core.Interfaces.Repositories;
using System.Net;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Responses;

namespace ApiContestNew.Application.Services
{
    public class LocationPointService : ILocationPointService
    {
        private readonly ILocationPointRepository _locationPointRepository;

        public LocationPointService(ILocationPointRepository locationPointRepository)
        {
            _locationPointRepository = locationPointRepository;
        }

        async public Task<ServiceResponse<LocationPoint>> GetPointAsync(long id)
        {
            if (id <= 0)
            {
                return new ServiceResponse400<LocationPoint>();
            }

            var point = await _locationPointRepository.GetPointByIdAsync(id);

            if (point == null)
            {
                return new ServiceResponse404<LocationPoint>();
            }

            return new ServiceResponse200<LocationPoint>(data: point);
        }

        async public Task<ServiceResponse<LocationPoint>> AddPointAsync(LocationPoint point)
        {
            if(!point.IsValidWithoutId())
            {
                return new ServiceResponse400<LocationPoint>();
            }

            var equalPoint = await _locationPointRepository.GetPointByCoordsAsync(point.Latitude, point.Longitude);

            if (equalPoint != null)
            {
                return new ServiceResponse409<LocationPoint>();
            }

            var createdPoint = await _locationPointRepository.AddPointAsync(point);

            return new ServiceResponse201<LocationPoint>(data: createdPoint);
        }

        async public Task<ServiceResponse<LocationPoint>> UpdatePointAsync(long id, LocationPoint point)
        {
            point.Id = id;
            if(!point.IsValid())
            {
                return new ServiceResponse400<LocationPoint>();
            }

            var editablePoint = await _locationPointRepository.GetPointByIdAsync(id);
            if(editablePoint == null)
            {
                return new ServiceResponse404<LocationPoint>();
            }

            if (editablePoint.AnimalVisitedLocation.Count > 0 ||
                editablePoint.ChippedAnimals.Count > 0)
            {
                return new ServiceResponse400<LocationPoint>();
            }

            var equalPoint = await _locationPointRepository.GetPointByCoordsAsync(point.Latitude, point.Longitude);
            if(equalPoint != null)
            {
                return new ServiceResponse409<LocationPoint>();
            }

            var editedPoint = await _locationPointRepository.UpdatePointAsync(point);

            return new ServiceResponse200<LocationPoint>(data: editedPoint);
        }

        async public Task<ServiceResponse<LocationPoint>> DeletePointAsync(long id)
        {
            if(id <= 0)
            {
                return new ServiceResponse400<LocationPoint>();
            }

            var point = await _locationPointRepository.GetPointByIdAsync(id);

            if (point == null)
            {
                return new ServiceResponse404<LocationPoint>();
            }

            if (point.ChippedAnimals.Count > 0 ||
                point.AnimalVisitedLocation.Count > 0)
            {
                return new ServiceResponse400<LocationPoint>();
            }

            await _locationPointRepository.DeletePointAsync(point);

            return new ServiceResponse200<LocationPoint>();
        }
    }
}
