namespace ApiContestNew.Core.Models.Filters
{
    public class AreaAnalyticsFilter
    {
        public DateTimeOffset? StartDate { get; set; } = null;
        public DateTimeOffset? EndDate { get; set; } = null;

        public bool IsValid()
        {
            if (StartDate != null &&
                EndDate != null &&
                StartDate >= EndDate)
            {
                return false;
            }

            return true;
        }
    }
}
