namespace ApiContestNew.Core.Models.Filters
{
    public class LocationPointFilter
    {
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public bool IsValid()
        {
            if (Latitude != null && Math.Abs((decimal)Latitude) > 90 ||
                Longitude != null && Math.Abs((decimal)Longitude) > 180)
            {
                return false;
            }

            return true;
        }
    }
}
