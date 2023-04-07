using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Dtos.Account;
using ApiContestNew.Dtos.AnimalType;
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
            CreateMap<UpdateAccountDto, Account>();

            // Animal type
            CreateMap<AnimalType, GetAnimalTypeDto>();
            CreateMap<AddAnimalTypeDto, AnimalType>();
            CreateMap<UpdateAnimalTypeDto, AnimalType>();

            // Location point
            CreateMap<LocationPoint, GetLocationPointDto>();
            CreateMap<AddLocationPointDto, LocationPoint>();
            CreateMap<UpdateLocationPointDto, LocationPoint>();
        }
    }
}
