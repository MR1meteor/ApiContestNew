using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Responses;

namespace ApiContestNew.Core.Interfaces.Services
{
    public interface IAnimalService
    {
        Task<ServiceResponse<Animal>> GetAnimalAsync(long id);
        //Task<ServiceResponse<List<Animal>>> GetAnimalsAsync(AnimalFilter animalFilter); TODO: Add filters aka specifications
        Task<ServiceResponse<Animal>> AddAnimalAsync(Animal animal);
        Task<ServiceResponse<Animal>> UpdateAnimalAsync(long id, Animal animal);
        Task<ServiceResponse<Animal>> DeleteAnimalAsync(long id);
        Task<ServiceResponse<Animal>> AddAnimalTypeToAnimalAsync(long animalId, long typeId);
        //Task<ServiceResponse<Animal>> UpdateAnimalTypeAtAnimalAsync(long id, ); TODO: Add Additional class (see in reference proj)
        Task<ServiceResponse<Animal>> DeleteAnimalTypeAtAnimalAsync(long animalId, long typeId);
    }
}
