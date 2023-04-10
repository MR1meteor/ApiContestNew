using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Dtos.Account;
using ApiContestNew.Dtos.AnimalType;
using ApiContestNew.Dtos.AnimalVisitedLocation;
using ApiContestNew.Dtos.LocationPoint;
using AutoMapper;

namespace ApiContestNew.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            // Account
            CreateMap<Account, GetAccountDto>();
            CreateMap<AddAccountDto, Account>();
            CreateMap<UpdateAccountDto, Account>();

            // Animal type
            CreateMap<AnimalType, GetAnimalTypeDto>();
            CreateMap<AddAnimalTypeDto, AnimalType>();
            CreateMap<UpdateAnimalTypeDto, AnimalType>();

            // Animal visited location
            CreateMap<AnimalVisitedLocation, GetAnimalVisitedLocationDto>();
            CreateMap<UpdateAnimalVisitedLocationDto, AnimalVisitedLocation>().ForMember(a => a.Id, opt => opt.MapFrom(z => z.VisitedLocationPointId));

            // Location point
            CreateMap<LocationPoint, GetLocationPointDto>();
            CreateMap<AddLocationPointDto, LocationPoint>();
            CreateMap<UpdateLocationPointDto, LocationPoint>();
        }
    }
}
