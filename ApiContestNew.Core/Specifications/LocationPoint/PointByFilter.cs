using ApiContestNew.Core.Models.Filters;

namespace ApiContestNew.Core.Specifications.LocationPoint
{
    public class PointByFilter : BaseSpecification<Models.Entities.LocationPoint>
    {
        public PointByFilter(LocationPointFilter filter)
            : base(p => 
            (filter.Latitude == null || filter.Latitude == p.Latitude) &&
            (filter.Longitude == null || filter.Longitude == p.Longitude))
        {
            AddInclude(p => p.AnimalVisitedLocation);
            AddInclude(p => p.ChippedAnimals);
        }
    }
}
