namespace ApiContestNew.Dtos.LocationPoint
{
    public record GetLocationPointDto
    {
        public long Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
