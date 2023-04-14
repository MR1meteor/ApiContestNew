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

            double equationIndex = 
                (((List<LocationPoint>)AreaPoints)[0].Longitude - ((List<LocationPoint>)AreaPoints)[1].Longitude) /
                (((List<LocationPoint>)AreaPoints)[0].Latitude - ((List<LocationPoint>)AreaPoints)[1].Latitude);
            double equationOffset = 
                ((List<LocationPoint>)AreaPoints)[1].Longitude - equationIndex * ((List<LocationPoint>)AreaPoints)[1].Latitude;

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
