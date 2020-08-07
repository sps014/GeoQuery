/*
 * All Test Cases are match result is matched result of
 * http://www.movable-type.co.uk/scripts/geohash.html 
 */

using System;
using Xunit;
using GeoQuery;
using System.Linq;

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
        public void InCircle()
        {
            var res=GeoQuery.GeoQuery.InCircleCheck(12, 77, 12.1, 77, 100);
            Assert.True(res);
            res = GeoQuery.GeoQuery.InCircleCheck(12, 77, 23, 87, 100);
            Assert.True(res);
        }
        [Fact]
        public void Centroid()
        {
            var expected= (15.0, 15.0);
            var res = GeoQuery.GeoQuery.GetCentroid(10, 10, 10, 10);
            Assert.Equal(expected, res);
        }
        [Fact]
        public void COnvertLatLong()
        {
            var expected = (12.008993216059187, 77.0091941298557);
            var res = GeoQuery.GeoQuery.ConvertToLatLong(1000.0, 1000.0, 12.0, 77.0);
            Assert.Equal(expected, res);
        }

        [Fact]
        public void CreateGeoHash()
        {
            var expected = new string[] {
                "tdnu20t9",
                "tdnu20t8",
                "tdnu20t3",
                "tdnu20t2",
                "tdnu20mz",
                "tdnu20mx",
                "tdnu20tc",
                "tdnu20tb",
                "tdnu20td",
                "tdnu20t"
            };
            var output = GeoQuery.GeoQuery.CreateGeoHash(new GeoPoint(12.0, 77.0), 20.0, 8, false, 1, 12);
            Assert.Equal(expected, output.ToArray());
        }
    }
}
