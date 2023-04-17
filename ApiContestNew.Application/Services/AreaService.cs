using ApiContestNew.Core.Interfaces.Repositories;
using ApiContestNew.Core.Interfaces.Services;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Responses;

namespace ApiContestNew.Application.Services
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _areaRepository;

        public AreaService(IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
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

            var areas = await _areaRepository.GetAllAsync();

            for (int i = 0; i < areas.Count; i++)
            {
                foreach (var point in area.AreaPoints)
                {
                    if (IsPointInsideArea(point, (List<LocationPoint>)areas[i].AreaPoints))
                    {
                        return new ServiceResponse400<Area>();
                    }
                }
            }

            var newArea = await _areaRepository.AddAreaAsync(area);

            return new ServiceResponse201<Area>(data: newArea);
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

            await _areaRepository.DeleteAreaAsync(area);

            return new ServiceResponse200<Area>();
        }

        private bool IsPointInsideArea(LocationPoint point, List<LocationPoint> areaPoints)
        {
            bool inside = false;

            //for (int i = 0; i < areaPoints.Count; i++)
            //{
            //    if (i == areaPoints.Count - 1)
            //    {
            //        if (areaPoints[i].Longitude >= point.Longitude && point.Longitude >= areaPoints[0].Longitude &&
            //            (
            //            areaPoints[i].Latitude >= point.Latitude && point.Latitude >= areaPoints[0].Latitude ||
            //            areaPoints[i].Latitude >= areaPoints[0].Latitude && point.Latitude >= areaPoints[i].Latitude ||
            //            areaPoints[0].Latitude >= point.Latitude && point.Latitude >= areaPoints[i].Latitude ||
            //            areaPoints[0].Latitude >= areaPoints[i].Latitude && point.Latitude >= areaPoints[0].Latitude
            //            ) ||

            //            areaPoints[0].Longitude >= point.Longitude && point.Longitude >= areaPoints[i].Longitude &&
            //            (
            //            areaPoints[i].Latitude >= point.Latitude && point.Latitude >= areaPoints[0].Latitude ||
            //            areaPoints[i].Latitude >= areaPoints[0].Latitude && point.Latitude >= areaPoints[i].Latitude ||
            //            areaPoints[0].Latitude >= point.Latitude && point.Latitude >= areaPoints[i].Latitude ||
            //            areaPoints[0].Latitude >= areaPoints[i].Latitude && point.Latitude >= areaPoints[0].Latitude
            //            )
            //           )
            //        {
            //            inside = !inside;
            //        }

            //        break;
            //    }
            //    else
            //    {
            //        if (areaPoints[i].Longitude >= point.Longitude && point.Longitude >= areaPoints[i + 1].Longitude &&
            //            (
            //            areaPoints[i].Latitude >= point.Latitude && point.Latitude >= areaPoints[i + 1].Latitude ||
            //            areaPoints[i].Latitude >= areaPoints[i + 1].Latitude && point.Latitude >= areaPoints[i].Latitude ||
            //            areaPoints[i + 1].Latitude >= point.Latitude && point.Latitude >= areaPoints[i].Latitude ||
            //            areaPoints[i + 1].Latitude >= areaPoints[i].Latitude && point.Latitude >= areaPoints[i + 1].Latitude
            //            ) ||

            //            areaPoints[i + 1].Longitude >= point.Longitude && point.Longitude >= areaPoints[i].Longitude &&
            //            (
            //            areaPoints[i].Latitude >= point.Latitude && point.Latitude >= areaPoints[i + 1].Latitude ||
            //            areaPoints[i].Latitude >= areaPoints[i + 1].Latitude && point.Latitude >= areaPoints[i].Latitude ||
            //            areaPoints[i + 1].Latitude >= point.Latitude && point.Latitude >= areaPoints[i].Latitude ||
            //            areaPoints[i + 1].Latitude >= areaPoints[i].Latitude && point.Latitude >= areaPoints[i + 1].Latitude
            //            )
            //           ) // TODO: My Fucking God... REDO THIS SOMEHOW!!!!!!
            //        {
            //            inside = !inside;
            //        }
            //    }
            //}

            //for (int i = 0; i < areaPoints.Count; i++)
            //{
            //    if (i == areaPoints.Count - 1)
            //    {
            //        if (areaPoints[i].Longitude >= point.Longitude && point.Longitude >= areaPoints[0].Longitude &&
            //            (
            //            areaPoints[i].Latitude > point.Latitude && point.Latitude > areaPoints[0].Latitude ||
            //            areaPoints[i].Latitude > areaPoints[0].Latitude && point.Latitude > areaPoints[i].Latitude ||
            //            areaPoints[0].Latitude > point.Latitude && point.Latitude > areaPoints[i].Latitude ||
            //            areaPoints[0].Latitude > areaPoints[i].Latitude && point.Latitude > areaPoints[0].Latitude
            //            ) ||

            //            areaPoints[0].Longitude > point.Longitude && point.Longitude >= areaPoints[i].Longitude &&
            //            (
            //            areaPoints[i].Latitude > point.Latitude && point.Latitude >= areaPoints[0].Latitude ||
            //            areaPoints[i].Latitude > areaPoints[0].Latitude && point.Latitude >= areaPoints[i].Latitude ||
            //            areaPoints[0].Latitude > point.Latitude && point.Latitude >= areaPoints[i].Latitude ||
            //            areaPoints[0].Latitude > areaPoints[i].Latitude && point.Latitude >= areaPoints[0].Latitude
            //            )
            //           )
            //        {
            //            inside = !inside;
            //        }

            //        break;
            //    }
            //    else
            //    {
            //        if (areaPoints[i].Longitude > point.Longitude && point.Longitude > areaPoints[i + 1].Longitude &&
            //            (
            //            areaPoints[i].Latitude > point.Latitude && point.Latitude > areaPoints[i + 1].Latitude ||
            //            areaPoints[i].Latitude > areaPoints[i + 1].Latitude && point.Latitude > areaPoints[i].Latitude ||
            //            areaPoints[i + 1].Latitude > point.Latitude && point.Latitude > areaPoints[i].Latitude ||
            //            areaPoints[i + 1].Latitude > areaPoints[i].Latitude && point.Latitude > areaPoints[i + 1].Latitude
            //            ) ||

            //            areaPoints[i + 1].Longitude > point.Longitude && point.Longitude >= areaPoints[i].Longitude &&
            //            (
            //            areaPoints[i].Latitude > point.Latitude && point.Latitude > areaPoints[i + 1].Latitude ||
            //            areaPoints[i].Latitude > areaPoints[i + 1].Latitude && point.Latitude > areaPoints[i].Latitude ||
            //            areaPoints[i + 1].Latitude > point.Latitude && point.Latitude > areaPoints[i].Latitude ||
            //            areaPoints[i + 1].Latitude > areaPoints[i].Latitude && point.Latitude > areaPoints[i + 1].Latitude
            //            )
            //           ) // TODO: My Fucking God... REDO THIS SOMEHOW!!!!!!
            //        {
            //            inside = !inside;
            //        }
            //    }
            //}

            for (int i = 0, j = areaPoints.Count - 1; i < areaPoints.Count; j = i++)
            {
                if ((((areaPoints[i].Longitude <= point.Longitude) && (point.Longitude < areaPoints[j].Longitude)) || ((areaPoints[j].Longitude <= point.Longitude) && (point.Longitude < areaPoints[i].Longitude))) && 
                    (((areaPoints[j].Longitude - areaPoints[i].Longitude) != 0)
                    && (point.Latitude > ((areaPoints[j].Latitude - areaPoints[i].Latitude) * (point.Longitude - areaPoints[i].Longitude)
                    / (areaPoints[j].Longitude - areaPoints[i].Longitude) + areaPoints[i].Latitude))))
                {
                    inside = !inside;
                }
            }

            return inside;
        }
    }
}
