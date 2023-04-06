namespace ApiContestNew.Dtos.AnimalType
{
    public record GetAnimalTypeDto
    {
        public long Id { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}
