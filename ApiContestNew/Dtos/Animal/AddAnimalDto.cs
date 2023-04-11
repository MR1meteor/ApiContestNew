namespace ApiContestNew.Dtos.Animal
{
    public record AddAnimalDto
    {
        public long[] AnimalTypes { get; set; } = Array.Empty<long>();
        public float Weight { get; set; }
        public float Length { get; set; }
        public float Height { get; set; }
        public string Gender { get; set; } = string.Empty;
        public int ChipperId { get; set; }
        public long ChippingLocationId { get; set; }
    }
}
