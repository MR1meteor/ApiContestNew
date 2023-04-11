namespace ApiContestNew.Core.Models.Filters
{
    public class AnimalFilter
    {
        public DateTimeOffset? StartDateTime { get; set; } = null;
        public DateTimeOffset? EndDateTime { get; set; } = null;
        public int? ChipperId { get; set; } = null;
        public long? ChippingLocationId { get; set; } = null;
        public string? LifeStatus { get; set; } = null;
        public string? Gender { get; set; } = null;
        public int From { get; set; } = 0;
        public int Size { get; set; } = 10;

        public AnimalFilter()
        {

        }

        public AnimalFilter(
            DateTimeOffset? startDateTime, DateTimeOffset? endDateTime,
            int? chipperId, long? chippingLocationId, string? lifeStatus,
            string? gender, int from, int size)
        {
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            ChipperId = chipperId;
            ChippingLocationId = chippingLocationId;
            LifeStatus = lifeStatus;
            Gender = gender;
            From = from;
            Size = size;
        }

        public bool IsValid()
        {
            string[] lifeStatuses = { "ALIVE", "DEAD" };
            string[] genders = { "MALE", "FEMALE", "OTHER" };

            if (From < 0 || Size <= 0 ||
                ChipperId <= 0 ||
                ChippingLocationId <= 0 ||
                LifeStatus != null && !lifeStatuses.Contains(LifeStatus) ||
                Gender != null && !genders.Contains(Gender))
            {
                return false;
            }

            return true;
        }
    }
}
