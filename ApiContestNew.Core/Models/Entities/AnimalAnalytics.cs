namespace ApiContestNew.Core.Models.Entities
{
    public class AnimalAnalytics : BaseEntity
    {
        public string AnimalType { get; set; } = string.Empty;
        public long AnimalTypeId { get; set; }
        public long QuantityAnimals { get; set; }
        public long AnimalsArrived { get; set; }
        public long AnimalsGone { get; set; }
    }
}
