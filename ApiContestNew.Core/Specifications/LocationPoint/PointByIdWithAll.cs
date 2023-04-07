namespace ApiContestNew.Core.Specifications.LocationPoint
{
    public class PointByIdWithAll : BaseSpecification<Models.Entities.LocationPoint>
    {
        public PointByIdWithAll(long id)
            : base(p => p.Id == id)
        {
            AddInclude(p => p.AnimalVisitedLocation);
            AddInclude(p => p.ChippedAnimals);
        }
    }
}
