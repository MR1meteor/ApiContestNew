namespace ApiContestNew.Core.Models.Entities
{
    public class Area : BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<LocationPoint> AreaPoints { get; set; } = new List<LocationPoint>();

        public Area()
        {

        }

        public Area(long id, string name, ICollection<LocationPoint> areaPoints)
        {
            Id = id;
            Name = name;
            AreaPoints = areaPoints;
        }

        public bool IsValid()
        {
            if (Id <= 0 || !IsValidWithoutId())
            {
                return false;
            }

            return true;
        }

        public bool IsValidWithoutId() // TODO: Add other requires validation
        {
            if (string.IsNullOrWhiteSpace(Name) ||
                AreaPoints == null ||
                AreaPoints.Count < 3 ||
                AreaPoints.GroupBy(x => x).Any(x => x.Count() > 1))
            {
                return false;
            }

            double y1 = ((List<LocationPoint>)AreaPoints)[0].Longitude;
            double y2 = ((List<LocationPoint>)AreaPoints)[1].Longitude;
            double x1 = ((List<LocationPoint>)AreaPoints)[0].Latitude;
            double x2 = ((List<LocationPoint>)AreaPoints)[1].Latitude;

            if (y1 - y2 == 0)
            {
                if (((List<LocationPoint>)AreaPoints)[2].Longitude == y1)
                {
                    return false;
                }
            }

            if (x1 - x2 == 0)
            {
                if (((List<LocationPoint>)AreaPoints)[2].Latitude == x1)
                {
                    return false;
                }
            }

            double equationIndex = (y1 - y2) / (x1 - x2);
            double equationOffset = y2 - equationIndex * x2;

            bool oneLine = true;
            foreach (var point in AreaPoints)
            {
                if (oneLine &&
                    point.Longitude != equationIndex * point.Latitude + equationOffset)
                {
                    oneLine = false;
                }

                if (point == null ||
                    !point.IsValidWithoutId())
                {
                    return false;
                }
            }

            if (oneLine)
            {
                return false;
            }

            return true;
        }
    }
}
