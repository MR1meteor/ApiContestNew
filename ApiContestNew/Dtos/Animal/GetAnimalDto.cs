namespace ApiContestNew.Dtos.Animal
{
    public record GetAnimalDto
    {
        public long Id { get; set; }
        public long[] AnimalTypes { get; set; } = Array.Empty<long>();
        public float Weight { get; set; }
        public float Length { get; set; }
        public float Height { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string LifeStatus { get; set; } = string.Empty;
        public DateTimeOffset ChippingDateTime { get; set; }
        public int ChipperId { get; set; }
        public long ChippingLocationId { get; set; }
        public long[] VisitedLocations { get; set; } = Array.Empty<long>();
        public DateTimeOffset? DeathDateTime { get; set; }
    }
}
