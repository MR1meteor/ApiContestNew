namespace ApiContestNew.Core.Specifications.AnimalType
{
    public class TypeByIdWithAnimals : BaseSpecification<Models.Entities.AnimalType>
    {
        public TypeByIdWithAnimals(long id)
            : base(t => t.Id == id)
        {
            AddInclude(t => t.Animals);
        }
    }
}
