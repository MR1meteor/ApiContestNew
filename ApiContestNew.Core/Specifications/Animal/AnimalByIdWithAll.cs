namespace ApiContestNew.Core.Specifications.Animal
{
    public class AnimalByIdWithAll : BaseSpecification<Models.Entities.Animal>
    {
        public AnimalByIdWithAll(long id)
            : base(a => a.Id == id)
        {
            AddInclude(a => a.Chipper);
            AddInclude(a => a.ChippingLocation);
            AddInclude(a => a.AnimalTypes);
            AddInclude(a => a.VisitedLocations);
        }
    }
}
