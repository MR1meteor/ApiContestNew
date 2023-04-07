using Microsoft.EntityFrameworkCore;

namespace ApiContestNew.Core.Models.Entities
{
    public class AnimalVisitedLocation : BaseEntity
    {
        public long Id { get; set; }
        [Precision(6)]
        public DateTimeOffset DateTimeOfVisitLocationPoint { get; set; }

        public long LocationPointId { get; set; }
        public LocationPoint LocationPoint { get; set; } = new();

        public ICollection<Animal> Animals { get; set; } = new List<Animal>();
    }
}
