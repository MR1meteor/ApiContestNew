using ApiContestNew.Dtos.LocationPoint;

namespace ApiContestNew.Dtos.Area
{
    public record AddAreaDto
    {
        public string Name { get; set; } = string.Empty;
        public List<AddLocationPointDto> AreaPoints { get; set; } = new();
    }
}
