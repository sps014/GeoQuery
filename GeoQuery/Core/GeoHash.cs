﻿///port of https://github.com/chrisveness/latlon-geohash/blob/master/latlon-geohash.js
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace GeoQuery
{
    public static class GeoHash
    {
        internal const string base32 = "0123456789bcdefghjkmnpqrstuvwxyz"; // (geohash-specific) Base32 map

        static readonly Dictionary<CardinalDirection, string[]> neighbour = new Dictionary<CardinalDirection, string[]>()
            {
                {
                    CardinalDirection.North, new string[]
                    {
                        "p0r21436x8zb9dcf5h7kjnmqesgutwvy",
                        "bc01fg45238967deuvhjyznpkmstqrwx"
                    }
                },
                {
                    CardinalDirection.South, new string[]
                    {
                        "14365h7k9dcfesgujnmqp0r2twvyx8zb",
                        "238967debc01fg45kmstqrwxuvhjyznp"
                    }
                },
                {
                     CardinalDirection.East, new string[]
                     {
                         "bc01fg45238967deuvhjyznpkmstqrwx",
                         "p0r21436x8zb9dcf5h7kjnmqesgutwvy"
                     }
                },
                {
                     CardinalDirection.West, new string[]
                     {
                         "238967debc01fg45kmstqrwxuvhjyznp",
                         "14365h7k9dcfesgujnmqp0r2twvyx8zb"
                     }
                }
            };

        static readonly Dictionary<CardinalDirection, string[]> border = new Dictionary<CardinalDirection, string[]>()
            {
                {
                    CardinalDirection.North, new string[]
                    {
                        "prxz", "bcfguvyz"
                    }
                },
                {
                    CardinalDirection.South, new string[]
                    {
                        "028b", "0145hjnp"
                    }
                },
                {
                     CardinalDirection.East, new string[]
                     {
                         "bcfguvyz", "prxz"
                     }
                },
                {
                     CardinalDirection.West, new string[]
                     {
                         "0145hjnp", "028b"
                     }
                }
            };

        /// <summary>
        /// Encode given latitude and longitude to corresponding GeoHash
        /// </summary>
        /// <param name="lat">latitude of the Location</param>
        /// <param name="lon">longitude of the Location</param>
        /// <param name="precision">Number of characters in resulting geohash </param>
        /// <returns>corresponding geohash</returns>
        public static string Encode(double lat, double lon, int precision)
        {
            var idx = 0; // index into base32 map
            var bit = 0; // each char holds 5 bits
            var evenBit = true;
            var geohash =new StringBuilder(string.Empty);

            double latMin = -90, latMax = 90;
            double lonMin = -180, lonMax = 180;
            double lonMid, latMid;
            while (geohash.Length < precision)
            {
                if (evenBit)
                {
                    // bisect E-W longitude
                    lonMid = (lonMin + lonMax) / 2;
                    if (lon >= lonMid)
                    {
                        idx = (idx * 2) + 1;
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
                    latMid = (latMin + latMax) / 2.0;
                    if (lat >= latMid)
                    {
                        idx = (idx * 2) + 1;
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
                    geohash.Append(base32[idx]);
                    bit = 0;
                    idx = 0;
                }
            }

            return geohash.ToString();
        }

        /// <summary>
        /// Encode given latitude and longitude to corresponding GeoHash
        /// </summary>
        /// <param name="point">Geo Point location </param>
        /// <param name="precision">Number of characters in resulting geohash</param>
        /// <returns>corresponding geohash</returns>
        public static string Encode(GeoPoint point, int precision)
        {
            return Encode(point.Latitude, point.Longitude, precision);
        }

        /// <summary>
        /// Get Corresponding Approximated Latitude and Longitude
        /// </summary>
        /// <param name="geohash">geohash of location</param>
        /// <returns>A GeoPoint (Corresponding Lat,Long)</returns>
        public static GeoPoint Decode(string geohash)
        {
            var bounds = GetBound(geohash); // <-- the hard work

            // now just determine the centre of the cell...

            double latMin = bounds.Southwest.Latitude, lonMin = bounds.Southwest.Longitude;
            double latMax = bounds.Northeast.Latitude, lonMax = bounds.Northeast.Longitude;

            // cell centre
            var lat = (latMin + latMax) / 2.0;
            var lon = (lonMin + lonMax) / 2.0;

            // round to close to centre without excessive precision: ⌊2-log10(Δ°)⌋ decimal places
            var precLat = (int)Math.Floor(2 - Math.Log((latMax - latMin) / 2.302585092994046));
            var precLong = (int)Math.Floor(2 - Math.Log((lonMax - lonMin) / 2.302585092994046));

            lat = double.Parse(lat.ToString($"F{precLat}", CultureInfo.InvariantCulture));
            lon = double.Parse(lon.ToString($"F{precLong}", CultureInfo.InvariantCulture));

            return new GeoPoint(lat, lon);
        }

        /// <summary>
        /// Get the bound of the given geohash
        /// </summary>
        /// <param name="geohash">hash of location</param>
        /// <returns>SW,NE location</returns>
        public static GeoHashBound GetBound(string geohash)
        {
            geohash = geohash.ToLower();
            var evenBit = true;
            double latMin = -90, latMax = 90;
            double lonMin = -180, lonMax = 180;

            for (var i = 0; i < geohash.Length; i++)
            {
                var chr = geohash[i];
                var idx = base32.IndexOf(chr);
                if (idx == -1)
                    throw new Exception("Invalid geohash");

                for (var n = 4; n >= 0; n--)
                {
                    var bitN = idx >> n & 1;
                    if (evenBit)
                    {
                        // longitude
                        var lonMid = (lonMin + lonMax) / 2.0;
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
                        var latMid = (latMin + latMax) / 2.0;
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

            return new GeoHashBound(new GeoPoint(latMin, lonMin), new GeoPoint(latMax, lonMax));
        } 

        /// <summary>
        /// Get the adjacent geohash
        /// </summary>
        /// <param name="geohash">main geohash string</param>
        /// <param name="direction">direction from current geohash</param>
        /// <returns>geohash of Neighbouring cell</returns>
        public static string GetAdjacent(string geohash, CardinalDirection direction)
        {
            geohash = geohash.ToLower();

            if (geohash.Length == 0)
                throw new Exception("Invalid geohash");

            var lastCh = geohash[geohash.Length - 1];    // last character of hash
            var parent = geohash.Substring(0, geohash.Length - 1); // hash without last character

            var type = geohash.Length % 2;

            // check for edge-cases which don't share common prefix
            if (border[direction][type].IndexOf(lastCh) != -1 && parent != string.Empty)
            {
                parent = GetAdjacent(parent, direction);
            }

            // append letter for direction to parent
            return parent + base32[neighbour[direction][type].IndexOf(lastCh)];
        }

        /// <summary>
        /// Get all the neighbouring Geohashes [All 8]
        /// </summary>
        /// <param name="geohash">Current Geohash</param>
        /// <returns>Neigbour Object</returns>
        public static Neighbour GetNeighbours(string geohash)
        {
            var n = GetAdjacent(geohash, CardinalDirection.North);
            var ne = GetAdjacent(GetAdjacent(geohash, CardinalDirection.North), CardinalDirection.East);
            var e = GetAdjacent(geohash, CardinalDirection.East);
            var se = GetAdjacent(GetAdjacent(geohash, CardinalDirection.South), CardinalDirection.East);
            var s = GetAdjacent(geohash, CardinalDirection.South);
            var sw = GetAdjacent(GetAdjacent(geohash, CardinalDirection.South), CardinalDirection.West);
            var w = GetAdjacent(geohash, CardinalDirection.West);
            var nw = GetAdjacent(GetAdjacent(geohash, CardinalDirection.North), CardinalDirection.West);

            return new Neighbour(n, s, e, w, ne, se, nw, sw);
        }
    }
}
