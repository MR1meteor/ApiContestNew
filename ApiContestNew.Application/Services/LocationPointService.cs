using ApiContestNew.Core.Interfaces.Services;
using ApiContestNew.Core.Interfaces.Repositories;
using System.Net;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Responses;

namespace ApiContestNew.Application.Services
{
    public class LocationPointService : ILocationPointService
    {
        private readonly ILocationPointRepository _locationPointRepo;

        public LocationPointService(ILocationPointRepository locationPointRepo)
        {
            _locationPointRepo = locationPointRepo;
        }

        async public Task<ServiceResponse<LocationPoint>> GetPointAsync(long id)
        {
            if (id <= 0)
            {
                return new ServiceResponse400<LocationPoint>();
            }

            var point = await _locationPointRepo.GetPointByIdAsync(id);

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

            var equalPoint = await _locationPointRepo.GetPointByCoordsAsync(point.Latitude, point.Longitude);

            if (equalPoint != null)
            {
                return new ServiceResponse409<LocationPoint>();
            }

            var createdPoint = await _locationPointRepo.AddPointAsync(point);

            return new ServiceResponse201<LocationPoint>(data: createdPoint);
        }

        async public Task<ServiceResponse<LocationPoint>> UpdatePointAsync(long id, LocationPoint point)
        {
            point.Id = id;
            if(!point.IsValid())
            {
                return new ServiceResponse400<LocationPoint>();
            }

            var editablePoint = await _locationPointRepo.GetPointByIdAsync(id);
            if(editablePoint == null)
            {
                return new ServiceResponse404<LocationPoint>();
            }

            var equalPoint = await _locationPointRepo.GetPointByCoordsAsync(point.Latitude, point.Longitude);
            if(equalPoint != null)
            {
                return new ServiceResponse409<LocationPoint>();
            }

            var editedPoint = await _locationPointRepo.UpdatePointAsync(point);

            return new ServiceResponse200<LocationPoint>(data: editedPoint);
        }

        async public Task<ServiceResponse<LocationPoint>> DeletePointAsync(long id)
        {
            if(id <= 0)
            {
                return new ServiceResponse400<LocationPoint>();
            }

            var point = await _locationPointRepo.GetPointByIdAsync(id);

            if (point == null)
            {
                return new ServiceResponse404<LocationPoint>();
            }

            if (point.ChippedAnimals.Count > 0 ||
                point.AnimalVisitedLocation.Count > 0)
            {
                return new ServiceResponse400<LocationPoint>();
            }

            await _locationPointRepo.DeletePointAsync(point);

            return new ServiceResponse200<LocationPoint>();
        }
    }
}
