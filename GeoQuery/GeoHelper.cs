using System;
using System.Collections.Generic;
using System.Text;

namespace GeoQuery
{
    public static class GeoHelper
    {
        private static double ToRadians(double angleIn10thofaDegree)
        {
            return (angleIn10thofaDegree * Math.PI) / 180;
        }
        public static double Distance(GeoPoint point1, GeoPoint point2)
        {
            var lon1 = ToRadians(point1.Longitude);
            var lon2 = ToRadians(point2.Longitude);
            var lat1 = ToRadians(point1.Latitude);
            var lat2 = ToRadians(point2.Latitude);

            // Haversine formula  
            double dlon = lon2 - lon1;
            double dlat = lat2 - lat1;
            double a = Math.Pow(Math.Sin(dlat / 2), 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Pow(Math.Sin(dlon / 2), 2);

            double c = 2 * Math.Asin(Math.Sqrt(a));

            double r = 6371;

            return (c * r);
        }

    }

}
