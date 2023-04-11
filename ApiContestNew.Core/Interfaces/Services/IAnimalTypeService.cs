using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Responses;

namespace ApiContestNew.Core.Interfaces.Services
{
    public interface IAnimalTypeService
    {
        Task<ServiceResponse<AnimalType>> GetAnimalTypeAsync(long id);
        Task<ServiceResponse<List<AnimalType>>> GetAnimalTypesByIdsAsync(long[] ids);
        Task<ServiceResponse<AnimalType>> AddAnimalTypeAsync(AnimalType animalType);
        Task<ServiceResponse<AnimalType>> UpdateAnimalTypeAsync(long id, AnimalType animalType);
        Task<ServiceResponse<AnimalType>> DeleteAnimalTypeAsync(long id);
    }
}
