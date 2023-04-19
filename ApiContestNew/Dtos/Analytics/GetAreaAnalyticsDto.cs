using ApiContestNew.Core.Models.Entities;

namespace ApiContestNew.Dtos.Analytics
{
    public record GetAreaAnalyticsDto
    {
        public long TotalQuantityAnimals { get; set; }
        public long TotalAnimalsArrived { get; set; }
        public long TotalAnimalsGone { get; set; }
        public List<GetAnimalAnalyticsDto> AnimalAnalytics { get; set; } = new();
    }
}
