using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Responses;

namespace ApiContestNew.Core.Interfaces.Services
{
    public interface IAnimalTypeService
    {
        Task<ServiceResponse<AnimalType>> GetAnimalTypeAsync(long id);
        Task<ServiceResponse<AnimalType>> AddAnimalTypeAsync(AnimalType animalType);
        Task<ServiceResponse<AnimalType>> UpdateAnimalTypeAsync(long id, AnimalType animnalType);
        Task<ServiceResponse<AnimalType>> DeleteAnimalTypeAsync(long id);
    }
}
