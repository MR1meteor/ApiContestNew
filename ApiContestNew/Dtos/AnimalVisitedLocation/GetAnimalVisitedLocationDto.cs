namespace ApiContestNew.Dtos.AnimalVisitedLocation
{
    public record GetAnimalVisitedLocationDto
    {
        public long Id { get; set; }
        public DateTimeOffset DateTimeOfVisitLocationPoint { get; set; }
        public long LocationPointId { get; set; }
    }
}
