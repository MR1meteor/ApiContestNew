using ApiContestNew.Dtos.LocationPoint;

namespace ApiContestNew.Dtos.Area
{
    public record GetAreaDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<GetAreaLocationPointDto> AreaPoints { get; set; } = new();
    }
}
