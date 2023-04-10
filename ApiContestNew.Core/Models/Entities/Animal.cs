using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiContestNew.Core.Models.Entities
{
    public class Animal : BaseEntity
    {
        public long Id { get; set; }
        //[Column(TypeName = "float(18, 10)")]
        public float Weight { get; set; }
        //[Column(TypeName = "float(18, 10)")]
        public float Length { get; set; }
        //[Column(TypeName = "float(18, 10)")]
        public float Height { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string LifeStatus { get; set; } = string.Empty;
        //[Precision(6)]
        public DateTimeOffset ChippingDateTime { get; set; }
        //[Precision(6)]
        public DateTimeOffset? DeathDateTime { get; set; } = null;

        public long ChipperId { get; set; }
        public Account Chipper { get; set; } = new();

        public long ChippingLocationId { get; set; }
        public LocationPoint ChippingLocation { get; set; } = new();

        public ICollection<AnimalType> AnimalTypes { get; set; } = new List<AnimalType>();
        public ICollection<AnimalVisitedLocation> VisitedLocations { get; set; } = new List<AnimalVisitedLocation>();

        public bool IsAbleToAddVisitedLocation(LocationPoint point)
        {
            if (LifeStatus == "DEAD" || 
                VisitedLocations.Count <= 0 && ChippingLocation == point ||
                VisitedLocations.Count > 0 && VisitedLocations.Last().LocationPoint == point)
            {
                return false;
            }

            return true;
        }

        public bool IsAbleToUpdateVisitedLocation(AnimalVisitedLocation location, LocationPoint point)
        {
            int locationIndex = ((List<AnimalVisitedLocation>)VisitedLocations).IndexOf(location);

            if (VisitedLocations.First() == location && ChippingLocation == point ||

                locationIndex == 0 && locationIndex < VisitedLocations.Count - 1 &&
                VisitedLocations.ElementAt(locationIndex + 1).LocationPoint == point ||

                locationIndex == VisitedLocations.Count - 1 && locationIndex > 0 &&
                VisitedLocations.ElementAt(locationIndex - 1).LocationPoint == point ||
                
                locationIndex < VisitedLocations.Count - 1 && locationIndex > 0 &&
                (VisitedLocations.ElementAt(locationIndex + 1).LocationPoint == point ||
                VisitedLocations.ElementAt(locationIndex - 1).LocationPoint == point) ||
                
                location.LocationPoint == point)
            {
                return false;
            }

            return true;
        }

        public int NextLocationIndexIfEqual(AnimalVisitedLocation location)
        {
            int locationIndex = ((List<AnimalVisitedLocation>)VisitedLocations).IndexOf(location);

            if (locationIndex <  VisitedLocations.Count - 1 && locationIndex == 0 &&
                VisitedLocations.ElementAt(locationIndex + 1).LocationPoint == ChippingLocation)
            {
                return locationIndex + 1;
            }

            return -1;
        }
    }
}
