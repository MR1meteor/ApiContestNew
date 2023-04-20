using ApiContestNew.Core.Interfaces.Repositories;
using ApiContestNew.Core.Interfaces.Services;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Responses;

namespace ApiContestNew.Application.Services
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _areaRepository;
        private readonly ILocationPointRepository _locationPointRepository;

        public AreaService(
            IAreaRepository areaRepository,
            ILocationPointRepository locationPointRepository)
        {
            _areaRepository = areaRepository;
            _locationPointRepository = locationPointRepository;
        }

        async public Task<ServiceResponse<Area>> GetAreaAsync(long id)
        {
            if (id <= 0)
            {
                return new ServiceResponse400<Area>();
            }

            var area = await _areaRepository.GetAreaByIdAsync(id);

            if (area == null)
            {
                return new ServiceResponse404<Area>();
            }

            return new ServiceResponse200<Area>(data: area);
        }

        async public Task<ServiceResponse<Area>> AddAreaAsync(Area area)
        {
            if (!area.IsValidWithoutId())
            {
                return new ServiceResponse400<Area>();
            }

            var equalArea = await _areaRepository.GetAreaByNameAsync(area.Name);
            if (equalArea != null)
            {
                return new ServiceResponse409<Area>();
            }

            //var equalPoints = _ // TODO: Получить эквивалентные точки по кордам и сверить их кол-во Areas (Более 0 бросаем 400 ошибку)

            var areas = await _areaRepository.GetAllAsync();

            foreach (var thisArea in areas)
            {
                foreach (var point in area.AreaPoints)
                {
                    if (LocationPoint.IsPointInsideArea(point, (List<LocationPoint>)thisArea.AreaPoints))
                    {
                        return new ServiceResponse<Area>(data: thisArea, statusCode: System.Net.HttpStatusCode.BadRequest);
                    }
                }

                foreach (var point in thisArea.AreaPoints)
                {
                    if (LocationPoint.IsPointInsideArea(point, (List<LocationPoint>)area.AreaPoints))
                    {
                        return new ServiceResponse<Area>(data: thisArea, statusCode: System.Net.HttpStatusCode.BadRequest);
                    }
                }
            }

            var newArea = await _areaRepository.AddAreaAsync(area);

            return new ServiceResponse201<Area>(data: newArea);
        }

        async public Task<ServiceResponse<Area>> UpdateAreaAsync(long id, Area area)
        {
            area.Id = id;
            
            if (!area.IsValid())
            {
                return new ServiceResponse400<Area>();
            }

            var editableArea = await _areaRepository.GetAreaByIdAsync(id);
            if (editableArea == null)
            {
                return new ServiceResponse404<Area>();
            }

            var equalArea = await _areaRepository.GetAreaByNameAsync(area.Name);
            if (equalArea != null)
            {
                return new ServiceResponse409<Area>();
            }

            var areas = await _areaRepository.GetAllAsync();

            foreach (var thisArea in areas)
            {
                if (thisArea.Id == id)
                {
                    continue;
                }

                foreach (var point in area.AreaPoints)
                {
                    if (LocationPoint.IsPointInsideArea(point, (List<LocationPoint>)thisArea.AreaPoints))
                    {
                        return new ServiceResponse<Area>(data: thisArea, statusCode: System.Net.HttpStatusCode.BadRequest);
                    }
                }

                foreach (var point in thisArea.AreaPoints)
                {
                    if (LocationPoint.IsPointInsideArea(point, (List<LocationPoint>)area.AreaPoints))
                    {
                        return new ServiceResponse<Area>(data: thisArea, statusCode: System.Net.HttpStatusCode.BadRequest);
                    }
                }
            }

            var editedArea = await _areaRepository.UpdateAreaAsync(area);

            return new ServiceResponse200<Area>(data: editedArea);
        }

        async public Task<ServiceResponse<Area>> DeleteAreaAsync(long id)
        {
            if (id <= 0)
            {
                return new ServiceResponse400<Area>();
            }
            
            var area = await _areaRepository.GetAreaByIdAsync(id);
            if(area == null)
            {
                return new ServiceResponse404<Area>();
            }

            List<LocationPoint> areaPoints = (List<LocationPoint>)area.AreaPoints;

            await _areaRepository.DeleteAreaAsync(area);

            foreach (var point in areaPoints)
            {
                if (point.Areas.Count <= 0 &&
                    point.AnimalVisitedLocation.Count <= 0 &&
                    point.ChippedAnimals.Count <= 0)
                {
                    await _locationPointRepository.DeletePointAsync(point);
                }
            }

            return new ServiceResponse200<Area>();
        }
    }
}
