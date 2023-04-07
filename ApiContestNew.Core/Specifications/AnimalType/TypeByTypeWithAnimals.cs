namespace ApiContestNew.Core.Specifications.AnimalType
{
    public class TypeByTypeWithAnimals : BaseSpecification<Models.Entities.AnimalType>
    {
        public TypeByTypeWithAnimals(string type)
            : base(t => t.Type == type)
        {
            AddInclude(t => t.Animals);
        }
    }
}
