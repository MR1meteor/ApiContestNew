using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiContestNew.Core.Models.Entities
{
    public class Animal
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
    }
}
