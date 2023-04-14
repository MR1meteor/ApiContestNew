using ApiContestNew.Core.Interfaces.Repositories;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Dtos.Account;
using ApiContestNew.Dtos.Animal;
using ApiContestNew.Dtos.AnimalType;
using ApiContestNew.Dtos.AnimalVisitedLocation;
using ApiContestNew.Dtos.Area;
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
            CreateMap<AddAccountWithRoleDto, Account>();
            CreateMap<UpdateAccountDto, Account>();

            // Animal
            CreateMap<Animal, GetAnimalDto>()
                .ForMember(x => x.AnimalTypes, opt => opt.MapFrom(z => z.AnimalTypes.Select(s => s.Id).ToArray()))
                .ForMember(x => x.VisitedLocations, opt => opt.MapFrom(z => z.VisitedLocations.Select(s => s.Id).ToArray()));
            CreateMap<AddAnimalDto, Animal>().ForMember(x => x.AnimalTypes, opt => opt.Ignore());
            CreateMap<UpdateAnimalDto, Animal>();

            // Animal type
            CreateMap<AnimalType, GetAnimalTypeDto>();
            CreateMap<AddAnimalTypeDto, AnimalType>();
            CreateMap<UpdateAnimalTypeDto, AnimalType>();

            // Animal visited location
            CreateMap<AnimalVisitedLocation, GetAnimalVisitedLocationDto>();
            CreateMap<UpdateAnimalVisitedLocationDto, AnimalVisitedLocation>().ForMember(a => a.Id, opt => opt.MapFrom(z => z.VisitedLocationPointId));

            // Area
            CreateMap<Area, GetAreaDto>();

            // Location point
            CreateMap<LocationPoint, GetLocationPointDto>();
            CreateMap<LocationPoint, GetAreaLocationPointDto>();
            CreateMap<AddLocationPointDto, LocationPoint>();
            CreateMap<UpdateLocationPointDto, LocationPoint>();
        }
    }
}
