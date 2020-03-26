///https://github.com/chrisveness/latlon-geohash/blob/master/latlon-geohash.js
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoQuery
{
    public partial class GeoQuery
    {

        const string  base32 = "0123456789bcdefghjkmnpqrstuvwxyz"; // (geohash-specific) Base32 map

        internal static string Encode(double lat,double lon,int precision)
        {


            int idx = 0; // index into base32 map
            int bit = 0; // each char holds 5 bits
            bool evenBit = true;
            string geohash = "";

            double latMin = -90, latMax = 90;
            double lonMin = -180, lonMax = 180;

            while (geohash.Length < precision)
            {
                if (evenBit)
                {
                    // bisect E-W longitude
                    double lonMid = (lonMin + lonMax) / 2;
                    if (lon >= lonMid)
                    {
                        idx = idx * 2 + 1;
                        lonMin = lonMid;
                    }
                    else
                    {
                        idx = idx * 2;
                        lonMax = lonMid;
                    }
                }
                else
                {
                    // bisect N-S latitude
                    double latMid = (latMin + latMax) / 2;
                    if (lat >= latMid)
                    {
                        idx = idx * 2 + 1;
                        latMin = latMid;
                    }
                    else
                    {
                        idx = idx * 2;
                        latMax = latMid;
                    }
                }
                evenBit = !evenBit;

                if (++bit == 5)
                {
                    // 5 bits gives us a character: append it and start over
                    geohash += base32[idx];
                    bit = 0;
                    idx = 0;
                }
            }

            return geohash;
        }
        internal static string Encode(GeoPoint point,int precision)
        {
            return Encode(point.Latitude, point.Longitude, precision);
        }
        internal static Bound Bounds(string geohash)
        {
            geohash = geohash.ToLower();

        }
    }

    public struct GeoPoint
    {
        public double Latitude { get; }
        public double Longitude { get; }
        public GeoPoint(double latitude,double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
    public struct Bound
    {
        public GeoPoint SW { get; }
        public GeoPoint NE { get; }
        public Bound(GeoPoint sw,GeoPoint ne)
        {
            SW = sw;
            NE = ne;
        }
    }
}
