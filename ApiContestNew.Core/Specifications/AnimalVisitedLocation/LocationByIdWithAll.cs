namespace ApiContestNew.Core.Specifications.AnimalVisitedLocation
{
    public class LocationByIdWithAll : BaseSpecification<Models.Entities.AnimalVisitedLocation>
    {
        public LocationByIdWithAll(long id)
            : base(l => l.Id == id)
        {
            AddInclude(l => l.LocationPoint);
            AddInclude(l => l.Animals);
        }
    }
}
