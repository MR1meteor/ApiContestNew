using ApiContestNew.Core.Models.Filters;

namespace ApiContestNew.Core.Specifications.Animal
{
    public class AnimalByFilterWithAll : BaseSpecification<Models.Entities.Animal>
    {
        public AnimalByFilterWithAll(AnimalFilter filter)
            : base(a =>
            (filter.StartDateTime == null || filter.StartDateTime <= a.ChippingDateTime) &&
            (filter.EndDateTime == null || filter.EndDateTime >= a.ChippingDateTime) &&
            (filter.ChipperId == null || filter.ChipperId == a.ChipperId) &&
            (filter.ChippingLocationId == null || filter.ChippingLocationId == a.ChippingLocationId) &&
            (filter.LifeStatus == null || filter.LifeStatus == a.LifeStatus) &&
            (filter.Gender == null || filter.Gender == a.Gender))
        {
            AddSkip(filter.From);
            AddTake(filter.Size);
            AddInclude(a => a.AnimalTypes);
            AddInclude(a => a.VisitedLocations);
        }
    }
}
