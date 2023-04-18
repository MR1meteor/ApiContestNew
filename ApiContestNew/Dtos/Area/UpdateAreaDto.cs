using ApiContestNew.Dtos.LocationPoint;

namespace ApiContestNew.Dtos.Area
{
    public record UpdateAreaDto
    {
        public string Name { get; set; } = string.Empty;
        public List<AddLocationPointDto> AreaPoints { get; set; } = new();
    }
}
