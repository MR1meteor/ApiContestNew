namespace ApiContestNew.Core.Specifications.LocationPoint
{
    public class PointByCoordsWithAll : BaseSpecification<Models.Entities.LocationPoint>
    {
        public PointByCoordsWithAll(double latitude, double longitude)
            : base(p => p.Latitude == latitude && p.Longitude == longitude)
        {
            AddInclude(p => p.AnimalVisitedLocation);
            AddInclude(p => p.ChippedAnimals);
        }
    }
}
