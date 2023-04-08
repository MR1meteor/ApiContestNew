namespace ApiContestNew.Core.Models.Filters
{
    public class AnimalVisitedLocationFilter
    {
        public DateTimeOffset? StartDateTime { get; set; } = null;
        public DateTimeOffset? EndDateTime { get; set; } = null;
        public int From { get; set; } = 0;
        public int Size { get; set; } = 10;

        public AnimalVisitedLocationFilter()
        {

        }

        public AnimalVisitedLocationFilter(
            DateTimeOffset? startDateTime, 
            DateTimeOffset? endDateTime,
            int from, int size)
        {
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            From = from;
            Size = size;
        }

        public bool IsValid()
        {
            if (From < 0 || Size <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
