using System;
using Xunit;
using GeoQuery;

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
        }
        [Fact]
        public void DecodeFromCoords()
        {
            double lat_actual5= 25.787,long_actual5= -4.3291;
            var calculated8 = GeoHash.Decode("eusftqx9");
            Assert.Equal(calculated8.Latitude.ToString().Substring(0,6),lat_actual5.ToString());
            Assert.Equal(calculated8.Longitude.ToString().Substring(0, 7), long_actual5.ToString());
        }
    }
}
