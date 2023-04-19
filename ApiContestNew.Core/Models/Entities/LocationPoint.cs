﻿namespace ApiContestNew.Core.Models.Entities
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
        //for (int i = 0; i < areaPoints.Count; i++)
        //{
        //    if (i == areaPoints.Count - 1)
        //    {
        //        if (areaPoints[i].Longitude >= point.Longitude && point.Longitude >= areaPoints[0].Longitude &&
        //            (
        //            areaPoints[i].Latitude >= point.Latitude && point.Latitude >= areaPoints[0].Latitude ||
        //            areaPoints[i].Latitude >= areaPoints[0].Latitude && point.Latitude >= areaPoints[i].Latitude ||
        //            areaPoints[0].Latitude >= point.Latitude && point.Latitude >= areaPoints[i].Latitude ||
        //            areaPoints[0].Latitude >= areaPoints[i].Latitude && point.Latitude >= areaPoints[0].Latitude
        //            ) ||

        //            areaPoints[0].Longitude >= point.Longitude && point.Longitude >= areaPoints[i].Longitude &&
        //            (
        //            areaPoints[i].Latitude >= point.Latitude && point.Latitude >= areaPoints[0].Latitude ||
        //            areaPoints[i].Latitude >= areaPoints[0].Latitude && point.Latitude >= areaPoints[i].Latitude ||
        //            areaPoints[0].Latitude >= point.Latitude && point.Latitude >= areaPoints[i].Latitude ||
        //            areaPoints[0].Latitude >= areaPoints[i].Latitude && point.Latitude >= areaPoints[0].Latitude
        //            )
        //           )
        //        {
        //            inside = !inside;
        //        }

        //        break;
        //    }
        //    else
        //    {
        //        if (areaPoints[i].Longitude >= point.Longitude && point.Longitude >= areaPoints[i + 1].Longitude &&
        //            (
        //            areaPoints[i].Latitude >= point.Latitude && point.Latitude >= areaPoints[i + 1].Latitude ||
        //            areaPoints[i].Latitude >= areaPoints[i + 1].Latitude && point.Latitude >= areaPoints[i].Latitude ||
        //            areaPoints[i + 1].Latitude >= point.Latitude && point.Latitude >= areaPoints[i].Latitude ||
        //            areaPoints[i + 1].Latitude >= areaPoints[i].Latitude && point.Latitude >= areaPoints[i + 1].Latitude
        //            ) ||

        //            areaPoints[i + 1].Longitude >= point.Longitude && point.Longitude >= areaPoints[i].Longitude &&
        //            (
        //            areaPoints[i].Latitude >= point.Latitude && point.Latitude >= areaPoints[i + 1].Latitude ||
        //            areaPoints[i].Latitude >= areaPoints[i + 1].Latitude && point.Latitude >= areaPoints[i].Latitude ||
        //            areaPoints[i + 1].Latitude >= point.Latitude && point.Latitude >= areaPoints[i].Latitude ||
        //            areaPoints[i + 1].Latitude >= areaPoints[i].Latitude && point.Latitude >= areaPoints[i + 1].Latitude
        //            )
        //           )
        //        {
        //            inside = !inside;
        //        }
        //    }
        //}

        //for (int i = 0; i < areaPoints.Count; i++)
        //{
        //    if (i == areaPoints.Count - 1)
        //    {
        //        if (IsPointOnLine(point, areaPoints[i], areaPoints[0]))
        //        {
        //            continue;
        //        }

        //        if (areaPoints[i].Longitude >= point.Longitude && point.Longitude > areaPoints[0].Longitude &&
        //            (
        //            areaPoints[i].Latitude >= point.Latitude && point.Latitude > areaPoints[0].Latitude ||
        //            //areaPoints[i].Latitude >= areaPoints[0].Latitude && point.Latitude > areaPoints[i].Latitude ||
        //            areaPoints[0].Latitude >= point.Latitude && point.Latitude > areaPoints[i].Latitude //||
        //            //areaPoints[0].Latitude >= areaPoints[i].Latitude && point.Latitude > areaPoints[0].Latitude
        //            ) ||

        //            areaPoints[0].Longitude >= point.Longitude && point.Longitude > areaPoints[i].Longitude &&
        //            (
        //            areaPoints[i].Latitude >= point.Latitude && point.Latitude > areaPoints[0].Latitude ||
        //            //areaPoints[i].Latitude >= areaPoints[0].Latitude && point.Latitude > areaPoints[i].Latitude ||
        //            areaPoints[0].Latitude >= point.Latitude && point.Latitude > areaPoints[i].Latitude //||
        //            //areaPoints[0].Latitude >= areaPoints[i].Latitude && point.Latitude > areaPoints[0].Latitude
        //            )
        //           )
        //        {
        //            inside = !inside;
        //        }

        //        break;
        //    }
        //    else
        //    {
        //        if (IsPointOnLine(point, areaPoints[i], areaPoints[i + 1]))
        //        {
        //            continue;
        //        }

        //        if (areaPoints[i].Longitude >= point.Longitude && point.Longitude > areaPoints[i + 1].Longitude &&
        //            (
        //            areaPoints[i].Latitude >= point.Latitude && point.Latitude > areaPoints[i + 1].Latitude ||
        //            //areaPoints[i].Latitude >= areaPoints[i + 1].Latitude && point.Latitude > areaPoints[i].Latitude ||
        //            areaPoints[i + 1].Latitude >= point.Latitude && point.Latitude > areaPoints[i].Latitude //||
        //            //areaPoints[i + 1].Latitude >= areaPoints[i].Latitude && point.Latitude > areaPoints[i + 1].Latitude
        //            ) ||

        //            areaPoints[i + 1].Longitude >= point.Longitude && point.Longitude >= areaPoints[i].Longitude &&
        //            (
        //            areaPoints[i].Latitude >= point.Latitude && point.Latitude > areaPoints[i + 1].Latitude ||
        //            //areaPoints[i].Latitude >= areaPoints[i + 1].Latitude && point.Latitude > areaPoints[i].Latitude ||
        //            areaPoints[i + 1].Latitude >= point.Latitude && point.Latitude > areaPoints[i].Latitude //||
        //            //areaPoints[i + 1].Latitude >= areaPoints[i].Latitude && point.Latitude > areaPoints[i + 1].Latitude
        //            )
        //           )
        //        {
        //            inside = !inside;
        //        }
        //    }
        //}

        //for (int i = 0, j = areaPoints.Count - 1; i < areaPoints.Count; j = i++)
        //{
        //    if ((((areaPoints[i].Longitude <= point.Longitude) && (point.Longitude < areaPoints[j].Longitude)) || ((areaPoints[j].Longitude <= point.Longitude) && (point.Longitude < areaPoints[i].Longitude))) && 
        //        (((areaPoints[j].Longitude - areaPoints[i].Longitude) != 0)
        //        && (point.Latitude > ((areaPoints[j].Latitude - areaPoints[i].Latitude) * (point.Longitude - areaPoints[i].Longitude)
        //        / (areaPoints[j].Longitude - areaPoints[i].Longitude) + areaPoints[i].Latitude))))
        //    {
        //        inside = !inside;
        //    }
        //}

        //return inside;
    //}

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

        //bool IsPointInsidePolygon(LocationPoint point, List<LocationPoint> points)
        //{
        //    double N, S, S1, S2, S3;
        //    int i1, i2, n;
        //    bool flag = false;
        //    N = points.Count;
        //    for (n = 0; n < N; n++)
        //    {
        //        flag = false;
        //        i1 = n < N - 1 ? n + 1 : 0;
        //        while (flag == false)
        //        {
        //            i2 = i1 + 1;
        //            if (i2 >= N)
        //                i2 = 0;
        //            if (i2 == (n < N - 1 ? n + 1 : 0))
        //                break;
        //            S = Math.Abs(points[i1].Latitude * (points[i2].Longitude - points[n].Longitude) +
        //                     points[i2].Latitude * (points[n].Longitude - points[i1].Longitude) +
        //                     points[n].Latitude * (points[i1].Longitude - points[i2].Longitude));
        //            S1 = Math.Abs(points[i1].Latitude * (points[i2].Longitude - point.Longitude) +
        //                      points[i2].Latitude * (point.Longitude - points[i1].Longitude) +
        //                      point.Latitude * (points[i1].Longitude - points[i2].Longitude));
        //            S2 = Math.Abs(points[n].Latitude * (points[i2].Longitude - point.Longitude) +
        //                      points[i2].Latitude * (point.Longitude - points[n].Longitude) +
        //                      point.Latitude * (points[n].Longitude - points[i2].Longitude));
        //            S3 = Math.Abs(points[i1].Latitude * (points[n].Longitude - point.Longitude) +
        //                      points[n].Latitude * (point.Longitude - points[i1].Longitude) +
        //                      point.Latitude * (points[i1].Longitude - points[n].Longitude));
        //            if (S == S1 + S2 + S3)
        //            {
        //                flag = true;
        //                break;
        //            }
        //            i1 = i1 + 1;
        //            if (i1 >= N)
        //            {
        //                i1 = 0;
        //                break;
        //            }
        //        }
        //        if (flag == false)
        //            break;
        //    }
        //    return flag;
        //}

        //bool checking(LocationPoint point, List<LocationPoint> points)
        //{
        //    bool inside = false;
        //    for (int i = 0; i < points.Count; i++)
        //    {
        //        if (i < points.Count - 1)
        //        {
        //            if (check(points[i], points[i + 1], point) != 0)
        //            {
        //                inside = !inside;
        //            }
        //        }
        //        else
        //        {
        //            if (check(points[i], points[0], point) != 0)
        //            {
        //                inside = !inside;
        //            }
        //        }
        //    }

        //    return inside;
        //}

        //int check(LocationPoint a, LocationPoint b, LocationPoint p)
        //{
        //    double ax = a.Latitude - p.Latitude;
        //    double ay = a.Longitude - p.Longitude;
        //    double bx = b.Latitude - p.Latitude;
        //    double by = b.Longitude - p.Longitude;
        //    if (ay * by > 0)
        //        return 1;
        //    int s = Math.Sign(ax * by - ay * bx);
        //    if (s == 0)
        //    {
        //        if (ax * bx <= 0)
        //            return 0;
        //        return 1;
        //    }
        //    if (ay < 0)
        //        return -s;
        //    if (by < 0)
        //        return s;
        //    return 1;
        //}

        //bool check2(LocationPoint point, List<LocationPoint> p)
        //{
        //    bool result = false;
        //    int j = p.Count - 1;
        //    for (int i = 0; i < p.Count; i++)
        //    {
        //        if ((p[i].Longitude < point.Longitude && p[j].Longitude >= point.Longitude || p[j].Longitude < point.Longitude && p[i].Longitude >= point.Longitude) &&
        //             (p[i].Latitude + (point.Longitude - p[i].Longitude) / (p[j].Longitude - p[i].Longitude) * (p[j].Latitude - p[i].Latitude) < point.Latitude))
        //            result = !result;
        //        j = i;
        //    }

        //    return result;
        //}

        //bool IsPointInsidePolygon(LocationPoint testPoint, List<LocationPoint> polygonPoints)
        //{
        //    int polygonSidesCount = polygonPoints.Count;
        //    bool isInside = false;

        //    for (int i = 0, j = polygonSidesCount - 1; i < polygonSidesCount; j = i++)
        //    {
        //        if (((polygonPoints[i].Latitude > testPoint.Latitude) != (polygonPoints[j].Latitude > testPoint.Latitude)) &&
        //            (testPoint.Longitude < (polygonPoints[j].Longitude - polygonPoints[i].Longitude) * (testPoint.Latitude - polygonPoints[i].Latitude) / (polygonPoints[j].Latitude - polygonPoints[i].Latitude) + polygonPoints[i].Longitude))
        //        {
        //            isInside = !isInside;
        //        }
        //    }

        //    return isInside;
        //}

        //static bool IsPointInsidePolygons(LocationPoint testPoint, List<Area> polygons)
        //{
        //    int insidePolygonsCount = 0;

        //    for (int k = 0; k < polygons.Count; k++)
        //    {
        //        List<LocationPoint> polygonPoints = (List<LocationPoint>)polygons[k].AreaPoints;
        //        int polygonSidesCount = polygonPoints.Count;
        //        bool isInside = false;

        //        for (int i = 0, j = polygonSidesCount - 1; i < polygonSidesCount; j = i++)
        //        {
        //            if (((polygonPoints[i].Latitude > testPoint.Latitude) != (polygonPoints[j].Latitude > testPoint.Latitude)) &&
        //                (testPoint.Longitude < (polygonPoints[j].Longitude - polygonPoints[i].Longitude) * (testPoint.Latitude - polygonPoints[i].Latitude) / (polygonPoints[j].Latitude - polygonPoints[i].Latitude) + polygonPoints[i].Longitude))
        //            {
        //                isInside = !isInside;
        //            }
        //        }

        //        if (isInside)
        //        {
        //            insidePolygonsCount++;
        //        }
        //    }

        //    return insidePolygonsCount > 0;
        //}
    }
}
