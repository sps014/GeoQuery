using GeoQuery.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace GeoQuery
{
    public class GeoQuery
    {
        public const double RadiusEarth = 6371000;
        public const double PI = 3.14159265359;
        public static bool InCircleCheck(double pointLat,double pointLong, double centralPointLat,double centralPointLong, double radius)
        {
            var x_diff = pointLong - centralPointLong;
            var y_diff = pointLat - centralPointLat;
            return Math.Pow(x_diff, 2) + Math.Pow(y_diff, 2) <= Math.Pow(radius, 2);
        }
        public static (double,double) GetCentroid(double pointLat, double pointLong, double height, double width)
        {
            var y_cen = pointLat + (height / 2);
            var x_cen = pointLong + (width / 2);

            return (x_cen, y_cen);
        }
        public static (double,double) ConvertToLatLong(double x, double y, double pointLat, double pointLong)
        {
            var lat_diff = y / RadiusEarth * (180 / PI);
            var lon_diff = x / RadiusEarth * (180 / PI) / Math.Cos(pointLat * PI / 180);

            var final_lat = pointLat + lat_diff;
            var final_lon = pointLong + lon_diff;

            return (final_lat, final_lon);
        }
        public static HashSet<string> CreateGeoHash(GeoPoint point, double radius, int precision, bool georaptor_flag = true, int minlevel = 1, int maxlevel = 12)
        {
            var x = 0.0;
            var y = 0.0;

            List<GeoPoint> Points = new List<GeoPoint>();
            HashSet<string> geoHashes = new HashSet<string>();
            var grid_width  = new double[]{ 5009400.0, 1252300.0, 156500.0, 39100.0, 4900.0, 1200.0, 152.9, 38.2, 4.8, 1.2, 0.149, 0.0370};
            var grid_height = new double[]{ 4992600.0, 624100.0, 156000.0, 19500.0, 4900.0, 609.4, 152.4, 19.0, 4.8, 0.595, 0.149, 0.0199};
            
            var height = grid_height[precision - 1] / 2;
            var width = grid_width[precision - 1] / 2;

            var lat_moves = (int)Math.Ceiling(radius / height);
            var lon_moves = (int)Math.Ceiling(radius / width);

            for (int i = 0; i < lat_moves; i++)
            {
                var temp_lat = y + height * i;
                for (int j = 0; j < lon_moves; j++)
                {
                    var temp_lon = x + width * j;

                    if(InCircleCheck(temp_lat,temp_lon,y,x,radius))
                    {
                        var (x_cen, y_cen) = GetCentroid(temp_lat, temp_lon, height, width);


                        var (lat, lon) = ConvertToLatLong(y_cen, x_cen, point.Latitude, point.Longitude);
                        Points.Add(new GeoPoint(lat, lon));

                        var (lat1, lon1) = ConvertToLatLong(-y_cen, x_cen, point.Latitude, point.Longitude);
                        Points.Add(new GeoPoint(lat1, lon1));

                        var (lat2, lon2) = ConvertToLatLong(y_cen, -x_cen, point.Latitude, point.Longitude);
                        Points.Add(new GeoPoint(lat2, lon2));

                        var (lat3, lon3) = ConvertToLatLong(-y_cen, -x_cen, point.Latitude, point.Longitude);
                        Points.Add(new GeoPoint(lat3, lon3));
                    }

                }
            }

            foreach(var gpoint in Points)
            {
                geoHashes.Add(GeoHash.Encode(gpoint, precision));
            }

            if(georaptor_flag)
            {
                geoHashes=GeoRaptor.Compress(geoHashes, minlevel, maxlevel);
            }

            return geoHashes;
        }
        public static HashSet<string> GetNearbyHashes(GeoPoint point,double radius,int precision)
        {
            var mainHash = GeoHash.Encode(point, precision);
            var hashes = new HashSet<string>();

            var height = GeoBlockSize.BlockSize[precision].Height;
            var width = GeoBlockSize.BlockSize[precision].Width;
            var diagonal = MathF.Sqrt((float)((height * height) + (width * width)));


            var box = new GeoBoxProperty()
            {
                Diagonal = diagonal,
                Width=width,
                Height=height
            };

            CalculateBlockNeighbour(mainHash, radius, precision, 0, ref hashes,ref box);

            return hashes;

        }
        internal struct GeoBoxProperty
        {
            public double Width { get; set; }
            public double Height { get; set; }
            public double Diagonal { get; set; }
        }
        private static void CalculateBlockNeighbour(string hash,double radius,int precision,double totalDistance,ref HashSet<string> outputHashes,ref GeoBoxProperty box)
        {
            if (totalDistance > radius)
                return;


            outputHashes.Add(hash);

            var nbr = GeoHash.GetNeighbours(hash);

            if(!outputHashes.Contains(nbr.East))
            {
                CalculateBlockNeighbour(nbr.East, radius, precision, totalDistance +box.Height, ref outputHashes,ref box);
            }
            if (!outputHashes.Contains(nbr.West))
            {
                CalculateBlockNeighbour(nbr.West, radius, precision, totalDistance + box.Height, ref outputHashes, ref box);
            }
            if (!outputHashes.Contains(nbr.North))
            {
                CalculateBlockNeighbour(nbr.North, radius, precision, totalDistance + box.Width, ref outputHashes, ref box);
            }
            if (!outputHashes.Contains(nbr.South))
            {
                CalculateBlockNeighbour(nbr.South, radius, precision, totalDistance + box.Width, ref outputHashes, ref box);
            }
            if (!outputHashes.Contains(nbr.Northeast))
            {
                CalculateBlockNeighbour(nbr.Northeast, radius, precision, totalDistance + box.Diagonal, ref outputHashes, ref box);
            }
            if (!outputHashes.Contains(nbr.Northwest))
            {
                CalculateBlockNeighbour(nbr.Northwest, radius, precision, totalDistance + box.Diagonal, ref outputHashes, ref box);
            }
            if (!outputHashes.Contains(nbr.Southeast))
            {
                CalculateBlockNeighbour(nbr.Southeast, radius, precision, totalDistance + box.Diagonal, ref outputHashes, ref box);
            }
            if (!outputHashes.Contains(nbr.Southwest))
            {
                CalculateBlockNeighbour(nbr.Southwest, radius, precision, totalDistance + box.Diagonal, ref outputHashes, ref box);
            }


        }
    }
}
