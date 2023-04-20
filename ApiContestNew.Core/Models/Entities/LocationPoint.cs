namespace ApiContestNew.Core.Models.Entities
{
    public class LocationPoint : BaseEntity
    {
        public long Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public List<AnimalVisitedLocation> AnimalVisitedLocation { get; set; } = new();
        public List<Animal> ChippedAnimals { get; set; } = new();

        public ICollection<Area> Areas { get; set; } = new List<Area>();

        public LocationPoint()
        {

        }

        public LocationPoint(long id, double latitude, double longitude)
        {
            Id = id;
            Latitude = latitude;
            Longitude = longitude;
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
            if (Math.Abs(Latitude) > 90 ||
                Math.Abs(Longitude) > 180)
            {
                return false;
            }

            return true;
        }

        public static bool IsPointInsideArea(LocationPoint point, List<LocationPoint> areaPoints) // TODO: Redo this somehow
        {
            bool inside = false;

            double x = point.Latitude;
            double y = point.Longitude;
            double x1, y1, x2, y2;



            for (int i = 0; i < areaPoints.Count; i++)
            {
                if (i < areaPoints.Count - 1)
                {
                    if (IsPointOnLine(point, areaPoints[i], areaPoints[i + 1]))
                    {
                        double x3 = i + 2 > areaPoints.Count - 1 ? areaPoints[0].Latitude : areaPoints[i + 2].Latitude;
                        double y3 = i + 2 > areaPoints.Count - 1 ? areaPoints[0].Longitude : areaPoints[i + 2].Longitude;

                        if (areaPoints[i].Latitude > point.Latitude && x3 > point.Latitude &&
                            (
                            areaPoints[i].Longitude > point.Longitude && point.Longitude > y3 ||
                            y3 > point.Longitude && point.Longitude > areaPoints[i].Longitude
                            )
                            )
                        {
                            i++;
                            inside = !inside;
                        }

                        continue;
                    }

                    x1 = areaPoints[i].Latitude;
                    y1 = areaPoints[i].Longitude;
                    x2 = areaPoints[i + 1].Latitude;
                    y2 = areaPoints[i + 1].Longitude;
                }
                else
                {
                    if (IsPointOnLine(point, areaPoints[i], areaPoints[0]))
                    {
                        continue;
                    }

                    x1 = areaPoints[i].Latitude;
                    y1 = areaPoints[i].Longitude;
                    x2 = areaPoints[0].Latitude;
                    y2 = areaPoints[0].Longitude;
                }

                if (y1 >= y && y >= y2 && y1 != y2 &&
                (
                x1 >= x && x >= x2 || x1 >= x2 && x2 >= x ||
                x2 >= x && x >= x1 || x2 >= x1 && x1 >= x
                ) ||

                y2 >= y && y >= y1 && y1 != y2 &&
                (
                x2 >= x && x >= x1 || x2 >= x1 && x1 >= x ||
                x1 >= x && x >= x2 || x1 >= x2 && x2 >= x
                )
                )
                {
                    if (y2 == y)
                    {
                        double y3 = i + 2 > areaPoints.Count - 1 ? areaPoints[0].Longitude : areaPoints[i + 2].Longitude;
                        if (y2 == areaPoints[0].Longitude && x2 == areaPoints[0].Latitude &&
                            (
                            y1 > y2 && y2 > areaPoints[1].Longitude ||
                            y1 < y2 && y2 < areaPoints[1].Longitude
                            )
                            )
                        {
                            inside = !inside;
                            continue;
                        }
                        else
                        {
                            i++;
                            if (y1 < y2 && y2 < y3 || y1 > y2 && y2 > y3)
                            {
                                inside = !inside;
                            }
                            continue;
                        }
                    }

                    if (y1 == y)
                    {
                        continue;
                    }

                    inside = !inside;
                }
            }

            return inside;
        }

        public static bool IsPointOnArea(LocationPoint point, Area area)
        {
            if (IsPointOnLine(point,
                ((List<LocationPoint>)area.AreaPoints)[area.AreaPoints.Count - 1],
                ((List<LocationPoint>)area.AreaPoints)[0]))
            {
                return true;
            }

            for (int i = 0; i < area.AreaPoints.Count - 1; i++)
            {
                if (IsPointOnLine(point,
                    ((List<LocationPoint>)area.AreaPoints)[i],
                    ((List<LocationPoint>)area.AreaPoints)[i + 1]))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsPointOnLine(LocationPoint point, LocationPoint p1, LocationPoint p2)
        {
            double h1 = Math.Pow(Math.Pow(p1.Latitude - point.Latitude, 2) + Math.Pow(p1.Longitude - point.Longitude, 2), 0.5);
            double h2 = Math.Pow(Math.Pow(p2.Latitude - point.Latitude, 2) + Math.Pow(p2.Longitude - point.Longitude, 2), 0.5);
            double h = Math.Pow(Math.Pow(p1.Latitude - p2.Latitude, 2) + Math.Pow(p1.Longitude - p2.Longitude, 2), 0.5);

            return h1 + h2 == h;
        }
    }
}
