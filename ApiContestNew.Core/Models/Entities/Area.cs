namespace ApiContestNew.Core.Models.Entities
{
    public class Area : BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<LocationPoint> AreaPoints { get; set; } = new List<LocationPoint>();
    }
}
