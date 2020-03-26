///https://github.com/chrisveness/latlon-geohash/blob/master/latlon-geohash.js
using System;
using System.Collections.Generic;
using System.Globalization;
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
        internal static GeoPoint Decode(string geohash)
        {
            Bound bounds = Bounds(geohash); // <-- the hard work
                                                    // now just determine the centre of the cell...

            double latMin = bounds.SW.Latitude, lonMin = bounds.SW.Longitude;
            double latMax = bounds.NE.Latitude, lonMax = bounds.NE.Longitude;

            // cell centre
            double lat = (latMin + latMax) / 2;
            double lon = (lonMin + lonMax) / 2;

            // round to close to centre without excessive precision: ⌊2-log10(Δ°)⌋ decimal places
            int precLat= (int) Math.Floor(2 - Math.Log((latMax - latMin)/ 2.302585092994046));
            int precLong = (int)Math.Floor(2 - Math.Log((lonMax - lonMin)/ 2.302585092994046));

            lat = double.Parse(lat.ToString($"F{precLat}", CultureInfo.InvariantCulture));
            lon= double.Parse(lon.ToString($"F{precLong}", CultureInfo.InvariantCulture));

            return new GeoPoint(lat, lon);
        }
        internal static Bound Bounds(string geohash)
        {
            geohash = geohash.ToLower();
            bool evenBit = true;
            double latMin = -90, latMax = 90;
            double lonMin = -180, lonMax = 180;

            for (int i = 0; i < geohash.Length; i++)
            {
                char chr = geohash[i];
                int idx = base32.IndexOf(chr);
                if (idx == -1) throw new Exception("Invalid geohash");

                for (int n = 4; n >= 0; n--)
                {
                    var bitN = idx >> n & 1;
                    if (evenBit)
                    {
                        // longitude
                        double lonMid = (lonMin + lonMax) / 2;
                        if (bitN == 1)
                        {
                            lonMin = lonMid;
                        }
                        else
                        {
                            lonMax = lonMid;
                        }
                    }
                    else
                    {
                        // latitude
                        double latMid = (latMin + latMax) / 2;
                        if (bitN == 1)
                        {
                            latMin = latMid;
                        }
                        else
                        {
                            latMax = latMid;
                        }
                    }
                    evenBit = !evenBit;
                }
            }

            return new Bound(new GeoPoint(latMin,lonMin),new GeoPoint(latMax,lonMax));
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
