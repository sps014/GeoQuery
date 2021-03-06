/*
 * All Test Cases are taken from
 * http://www.movable-type.co.uk/scripts/geohash.html 
 */

using System;
using Xunit;
using GeoQuery;
using System.Linq;
using System.Collections.Generic;

namespace GeoQueryTests
{
    public class GeoQueryTests
    {
        [Fact]
        public void EncodeFromCoords()
        {
            string actual8 = "eusftqx9";
            string calculated8 = GeoHash.Encode(25.78792, -4.32913, 8);
            Assert.Equal(actual8, calculated8);
            Assert.Equal("efkbt6rx", GeoHash.Encode(new GeoPoint(12.7578, -4.32913), 8));
            Assert.Equal("fbukqnpp", GeoHash.Encode(new GeoPoint(50, -50), 8));

        }
        [Fact]
        public void DecodeFromCoords()
        {
            double lat_actual5 = 25.787, long_actual5 = -4.3291;
            var calculated8 = GeoHash.Decode("eusftqx9");
            Assert.Equal(calculated8.Latitude.ToString().Substring(0, 6), lat_actual5.ToString());
            Assert.Equal(calculated8.Longitude.ToString().Substring(0, 7), long_actual5.ToString());
        }
        [Fact]
        public void DirectionEncodeTest()
        {
            /*
             * 
                eusftqx6	eusftqxd	eusftqxf
                eusftqx3	eusftqx9	eusftqxc
                eusftqx2	eusftqx8	eusftqxb

             */

            Assert.Equal("eusftqxd", GeoHash.GetAdjacent("eusftqx9", CardinalDirection.North));
            Assert.Equal("eusftqx8", GeoHash.GetAdjacent("eusftqx9", CardinalDirection.South));
            Assert.Equal("eusftqxc", GeoHash.GetAdjacent("eusftqx9", CardinalDirection.East));
            Assert.Equal("eusftqx3", GeoHash.GetAdjacent("eusftqx9", CardinalDirection.West));

        }
        [Fact]
        public void NeiboursTest()
        {
            /*
               efkbt6x2	efkbt6x8	efkbt6xb
               efkbt6rr	efkbt6rx	efkbt6rz
               efkbt6rq	efkbt6rw	efkbt6ry
             */

            var bounds = GeoHash.GetNeighbours("efkbt6rx");
            Assert.Equal("efkbt6x8", bounds.North);
            Assert.Equal("efkbt6rw", bounds.South);
            Assert.Equal("efkbt6rr", bounds.West);
            Assert.Equal("efkbt6rz", bounds.East);
            Assert.Equal("efkbt6ry", bounds.Southeast);
            Assert.Equal("efkbt6xb", bounds.Northeast);
            Assert.Equal("efkbt6rq", bounds.Southwest);
            Assert.Equal("efkbt6x2", bounds.Northwest);

        }

        [Fact]
        public void Dist()
        {
            var dist = GeoUtils.Distance(new GeoPoint(12, 11), new GeoPoint(11, 12));
            Assert.Equal("155.68179481457878", dist.ToString());
        }

        [Fact]
        public void CreateGeoHash()
        {
            var expected = new List<string> {
                "gbsuvkn", "gbsuvkp","gbsuvs0","gbsuv7y","gbsuv7z","gbsuveb","gbsuv7w","gbsuv7x","gbsuve8"
            };
            var output = GeoQuery.HashQuery.GetNearbyHashes(new GeoPoint(48.669, -4.32913), 217,7).ToList();
            output.Sort();
            expected.Sort();
            Assert.Equal(expected, output);
        }
    }
}
