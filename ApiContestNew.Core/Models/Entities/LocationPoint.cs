namespace ApiContestNew.Core.Models.Entities
{
    public class LocationPoint : BaseEntity
    {
        public long Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public List<AnimalVisitedLocation> AnimalVisitedLocation { get; set; } = new();
        public List<Animal> ChippedAnimals { get; set; } = new();

        public ICollection<Area> Areas { get; set; } = new List<Area>();

        public LocationPoint()
        {

        }

        public LocationPoint(long id, double latitude, double longitude)
        {
            Id = id;
            Latitude = latitude;
            Longitude = longitude;
        }

        public bool IsValid()
        {
            if (Id <= 0 || !IsValidWithoutId())
            {
                return false;
            }

            return true;
        }

        public bool IsValidWithoutId()
        {
            if (Math.Abs(Latitude) > 90 ||
                Math.Abs(Longitude) > 180)
            {
                return false;
            }

            return true;
        }
    }
}
