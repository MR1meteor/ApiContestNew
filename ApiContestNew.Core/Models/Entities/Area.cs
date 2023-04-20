using System.Drawing;

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

        public bool IsValidWithoutId()
        {
            if (string.IsNullOrWhiteSpace(Name) ||
                AreaPoints == null ||
                AreaPoints.Count < 3 ||
                AreaPoints.GroupBy(x => x).Any(x => x.Count() > 1))
            {
                return false;
            }

            for (int i = 0; i < AreaPoints.Count - 3; i++)
            {
                for (int j = i + 2; j < AreaPoints.Count - 1; j++)
                {
                    if (AreLinesCrossing(
                        ((List<LocationPoint>)AreaPoints)[i],
                        ((List<LocationPoint>)AreaPoints)[i + 1],
                        ((List<LocationPoint>)AreaPoints)[j],
                        ((List<LocationPoint>)AreaPoints)[j + 1]))
                    {
                        return false;
                    }
                }
            }

            foreach (var point in AreaPoints)
            {
                if (point == null ||
                    !point.IsValidWithoutId())
                {
                    return false;
                }
            }

            for (int i = 0; i < AreaPoints.Count - 2; i++)
            {
                if (OneLine(
                    ((List<LocationPoint>)AreaPoints)[i],
                    ((List<LocationPoint>)AreaPoints)[i + 1],
                    ((List<LocationPoint>)AreaPoints)[i + 2]))
                {
                    return false;
                }
            }

            return true;
        }

        private bool AreLinesCrossing(LocationPoint p1, LocationPoint p2, LocationPoint p3, LocationPoint p4) // TODO: Redo this (doesnt work)
        {
            double equationIndex1 = (p1.Longitude - p2.Longitude) / (p1.Latitude - p2.Latitude);
            double equationOffset1 = p2.Longitude - equationIndex1 * p2.Latitude;

            double equationIndex2 = (p3.Longitude - p4.Longitude) / (p3.Latitude - p4.Latitude);
            double equationOffset2 = p4.Longitude - equationIndex2 * p4.Latitude;

            try
            {
                var crossingLongitude = equationIndex1 * (equationOffset2 - equationOffset1) / (equationIndex1 - equationIndex2) + equationOffset1;
                var crossingLatitude = (equationOffset2 - equationOffset1) / (equationIndex1 - equationIndex2);

                if (LocationPoint.IsPointOnLine(new LocationPoint { Latitude = crossingLatitude, Longitude = crossingLongitude } ,p1, p2) &&
                    LocationPoint.IsPointOnLine(new LocationPoint { Latitude = crossingLatitude, Longitude = crossingLongitude }, p3, p4))
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        private bool OneLine(LocationPoint p1, LocationPoint p2, LocationPoint p3)
        {
            if (p1.Latitude == p2.Latitude && p1.Latitude == p3.Latitude)
            {
                return true;
            }

            if (p1.Longitude == p2.Longitude && p1.Longitude == p3.Longitude)
            {
                return true;
            }

            var leftPart = (p3.Longitude - p1.Longitude) / (p2.Longitude - p1.Longitude);
            var rightPart = (p3.Latitude - p1.Latitude) / (p2.Latitude - p1.Latitude);
            double precision = 0.01;

            if (leftPart + precision > rightPart && leftPart - precision < rightPart)
            {
                return true;
            }

            return false;
        }
    }
}
