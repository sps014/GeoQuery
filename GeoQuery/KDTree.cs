using System;
using System.Collections.Generic;
using System.Text;

namespace GeoQuery
{
    public class KDTree
    {
        private double Distance(GeoPoint point1,GeoPoint point2)
        {
            if ((point1.Latitude == point2.Latitude) && (point1.Longitude == point2.Longitude))
            {
                return 0;
            }
            else
            {
                double theta = point1.Longitude - point2.Longitude;
                double dist = Math.Sin(Deg2Rad(point1.Latitude)) * Math.Sin(Deg2Rad(point2.Latitude)) +
                              Math.Cos(Deg2Rad(point1.Latitude)) * Math.Cos(Deg2Rad(point1.Latitude)) * 
                              Math.Cos(Deg2Rad(theta));

                dist = Math.Acos(dist);
                dist = Rad2Deg(dist);
                dist = dist * 60 * 1.1515;
                dist = dist * 1.609344;

                return (dist);
            }
        }

        private double Deg2Rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }
        private double Rad2Deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }

    }
}
