using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Dtos.LocationPoint;
using AutoMapper;

namespace ApiContestNew.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            // Location point
            CreateMap<LocationPoint, GetLocationPointDto>();
            CreateMap<LocationPoint, UpdateLocationPointDto>();
            CreateMap<AddLocationPointDto, LocationPoint>();
        }
    }
}
