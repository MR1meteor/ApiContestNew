namespace ApiContestNew.Core.Entities
{
    public class LocationPoint
    {
        public long Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public List<AnimalVisitedLocation> AnimalVisitedLocation { get; set; } = new();
        public List<Animal> ChippedAnimals { get; set; } = new();
    }
}
