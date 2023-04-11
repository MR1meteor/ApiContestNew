using ApiContestNew.Core.Interfaces.Repositories;
using ApiContestNew.Core.Interfaces.Services;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Filters;
using ApiContestNew.Core.Models.Responses;

namespace ApiContestNew.Application.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ILocationPointRepository _locationPointRepository;
        private readonly IAnimalTypeRepository _animalTypeRepository;

        public AnimalService(
            IAnimalRepository animalRepository,
            IAccountRepository accountRepository,
            ILocationPointRepository locationPointRepository,
            IAnimalTypeRepository animalTypeRepository)
        {
            _animalRepository = animalRepository;
            _accountRepository = accountRepository;
            _locationPointRepository = locationPointRepository;
            _animalTypeRepository = animalTypeRepository;
        }

        public async Task<ServiceResponse<Animal>> GetAnimalAsync(long id)
        {
            if (id <= 0)
            {
                return new ServiceResponse400<Animal>();
            }

            var animal = await _animalRepository.GetAnimalByIdAsync(id);
            if (animal == null)
            {
                return new ServiceResponse404<Animal>();
            }

            return new ServiceResponse200<Animal>(data: animal);
        }

        public async Task<ServiceResponse<List<Animal>>> GetAnimalsAsync(AnimalFilter animalFilter)
        {
            if (!animalFilter.IsValid())
            {
                return new ServiceResponse400<List<Animal>>();
            }

            var animals = await _animalRepository.GetAnimalsByFilterAsync(animalFilter);

            return new ServiceResponse200<List<Animal>>(data: animals);
        }

        public async Task<ServiceResponse<Animal>> AddAnimalAsync(Animal animal)
        {
            if (!animal.IsValidWithoutId())
            {
                return new ServiceResponse400<Animal>();
            }

            if (animal.AnimalTypes.GroupBy(x => x).Any(x => x.Count() > 1))
            {
                return new ServiceResponse409<Animal>();
            }

            var account = await _accountRepository.GetAccountByIdAsync(animal.ChipperId);
            var point = await _locationPointRepository.GetPointByIdAsync(animal.ChippingLocationId);

            if (account == null || point == null)
            {
                return new ServiceResponse404<Animal>();
            }

            animal.Chipper = account;
            animal.ChippingLocation = point;
            var newAnimal = await _animalRepository.AddAnimalAsync(animal);

            return new ServiceResponse201<Animal>(data: newAnimal);
        }

        public async Task<ServiceResponse<Animal>> UpdateAnimalAsync(long id, Animal animal)
        {
            animal.Id = id;

            if (id <= 0 || !animal.IsValidForUpdate())
            {
                return new ServiceResponse400<Animal>();
            }

            var dbAnimal = await _animalRepository.GetAnimalByIdAsync(id);
            var chipper = await _accountRepository.GetAccountByIdAsync(animal.ChipperId);
            var point = await _locationPointRepository.GetPointByIdAsync(animal.ChippingLocationId);

            if (dbAnimal == null ||
                chipper == null ||
                point == null)
            {
                return new ServiceResponse404<Animal>();
            }

            if (dbAnimal.LifeStatus == "DEAD" &&  animal.LifeStatus == "ALIVE" ||
                dbAnimal.VisitedLocations.Count > 0 &&
                dbAnimal.VisitedLocations.First().LocationPointId == animal.ChippingLocationId)
            {
                return new ServiceResponse400<Animal>();
            }

            animal.Chipper = chipper;
            animal.ChippingLocation = point;

            var editedAnimal = await _animalRepository.UpdateAnimalAsync(animal);

            return new ServiceResponse200<Animal>(data: editedAnimal);
        }

        public async Task<ServiceResponse<Animal>> DeleteAnimalAsync(long id)
        {
            if (id <= 0)
            {
                return new ServiceResponse400<Animal>();
            }

            var animal = await _animalRepository.GetAnimalByIdAsync(id);
            if (animal == null)
            {
                return new ServiceResponse404<Animal>();
            }

            if (animal.VisitedLocations.Count > 0 &&
                animal.VisitedLocations.Last().LocationPointId != animal.ChippingLocationId)
            {
                return new ServiceResponse400<Animal>();
            }

            await _animalRepository.DeleteAnimalAsync(animal);

            return new ServiceResponse200<Animal>();
        }

        public async Task<ServiceResponse<Animal>> AddAnimalTypeToAnimalAsync(long animalId, long typeId)
        {
            if (animalId <= 0 || typeId <= 0)
            {
                return new ServiceResponse400<Animal>();
            }

            var animal = await _animalRepository.GetAnimalByIdAsync(animalId);
            var type = await _animalTypeRepository.GetTypeByIdAsync(typeId);

            if (animal == null || type == null)
            {
                return new ServiceResponse404<Animal>();
            }

            if (animal.AnimalTypes.Contains(type))
            {
                return new ServiceResponse409<Animal>();
            }

            var editedAnimal = await _animalRepository.AddAnimalTypeToAnimalAsync(animal, type);

            return new ServiceResponse201<Animal>(data: editedAnimal);
        }

        public async Task<ServiceResponse<Animal>> UpdateAnimalTypeAtAnimalAsync(long animalId, long oldTypeId, long newTypeId)
        {
            if (animalId <= 0 ||  oldTypeId <= 0 || newTypeId <= 0)
            {
                return new ServiceResponse400<Animal>();
            }

            var animal = await _animalRepository.GetAnimalByIdAsync(animalId);
            var oldType = await _animalTypeRepository.GetTypeByIdAsync(oldTypeId);
            var newType = await _animalTypeRepository.GetTypeByIdAsync(newTypeId);

            if (animal == null || oldType == null ||
                newType == null || !animal.AnimalTypes.Contains(oldType))
            {
                return new ServiceResponse404<Animal>();
            }

            if (animal.AnimalTypes.Contains(newType))
            {
                return new ServiceResponse409<Animal>();
            }

            var editedAnimal = await _animalRepository.UpdateAnimalTypeAtAnimalAsync(animal, oldType, newType);

            return new ServiceResponse200<Animal>(data: editedAnimal);
        }

        public async Task<ServiceResponse<Animal>> DeleteAnimalTypeAtAnimalAsync(long animalId, long typeId)
        {
            if (animalId <= 0 || typeId <= 0)
            {
                return new ServiceResponse400<Animal>();
            }

            var animal = await _animalRepository.GetAnimalByIdAsync(animalId);
            var type = await _animalTypeRepository.GetTypeByIdAsync(typeId);

            if (animal == null || type == null ||
                !animal.AnimalTypes.Contains(type))
            {
                return new ServiceResponse404<Animal>();
            }

            if (animal.AnimalTypes.Count == 1)
            {
                return new ServiceResponse400<Animal>();
            }

            var editedAnimal = await _animalRepository.DeleteAnimalTypeAtAnimalAsync(animal, type);

            return new ServiceResponse200<Animal>(data: editedAnimal);
        }
    }
}
