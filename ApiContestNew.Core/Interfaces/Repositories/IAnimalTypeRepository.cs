using ApiContestNew.Core.Models.Entities;

namespace ApiContestNew.Core.Interfaces.Repositories
{
    public interface IAnimalTypeRepository
    {
        Task<AnimalType?> GetTypeByIdAsync(long id);
        Task<List<AnimalType>> GetTypesByIdsAsync(long[] ids);
        Task<AnimalType?> GetTypeByTypeAsync(string type);
        Task<AnimalType?> AddTypeAsync(AnimalType animalType);
        Task<AnimalType?> UpdateTypeAsync(AnimalType animalType);
        Task<AnimalType?> DeleteTypeAsync(AnimalType animalType);
    }
}
