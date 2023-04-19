
namespace ApiContestNew.Core.Specifications.AnimalVisitedLocation
{
    public class LocationsByTimeWithAll : BaseSpecification<Models.Entities.AnimalVisitedLocation>
    {
        public LocationsByTimeWithAll(DateTimeOffset? startTime, DateTimeOffset? endTime)
            : base(l => 
            (startTime == null || startTime <= l.DateTimeOfVisitLocationPoint) &&
            (endTime == null || endTime >= l.DateTimeOfVisitLocationPoint))
        {
            AddInclude(l => l.Animal);
            AddInclude(l => l.LocationPoint);
        }
    }
}
