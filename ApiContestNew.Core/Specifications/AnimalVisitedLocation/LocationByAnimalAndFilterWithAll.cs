using ApiContestNew.Core.Models.Filters;

namespace ApiContestNew.Core.Specifications.AnimalVisitedLocation
{
    public class LocationByAnimalAndFilterWithAll : BaseSpecification<Models.Entities.AnimalVisitedLocation>
    {
        public LocationByAnimalAndFilterWithAll(Models.Entities.Animal animal, AnimalVisitedLocationFilter filter)
            : base(l =>
            l.Animal == animal &&
            (filter.StartDateTime == null || filter.StartDateTime <= l.DateTimeOfVisitLocationPoint) &&
            (filter.EndDateTime == null || filter.EndDateTime >= l.DateTimeOfVisitLocationPoint))
        {
            AddSkip(filter.From);
            AddTake(filter.Size);
            AddOrderBy(l => l.DateTimeOfVisitLocationPoint);
        }
    }
}
