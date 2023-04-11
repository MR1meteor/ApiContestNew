namespace ApiContestNew.Dtos.Animal
{
    public record UpdateAnimalDto
    {
        public float Weight { get; set; }
        public float Length { get; set; }
        public float Height { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string LifeStatus { get; set; } = string.Empty;
        public int ChipperId { get; set; }
        public long ChippingLocationId { get; set; }
    }
}
