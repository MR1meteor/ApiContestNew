using ApiContestNew.Core.Models.Entities;

namespace ApiContestNew.Core.Interfaces.Repositories
{
    public interface IAnimalRepository
    {
        Task<Animal?> GetAnimalByIdAsync(long id);
        Task<Animal?> AddVisitedLocationToAnimalAsync(Animal animal, AnimalVisitedLocation location);
    }
}
