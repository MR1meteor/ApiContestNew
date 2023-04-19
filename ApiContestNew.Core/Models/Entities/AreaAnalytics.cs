namespace ApiContestNew.Core.Models.Entities
{
    public class AreaAnalytics : BaseEntity
    {
        public long TotalQuantityAnimals { get; set; }
        public long TotalAnimalsArrived { get; set; }
        public long TotalAnimalsGone { get; set; }
        public List<AnimalAnalytics> AnimalAnalytics { get; set; } = new();
    }
}
