using System;

namespace GeoQuery
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = GeoQuery.Encode(26.83710, 80.92060, 6);
            var sp = GeoQuery.Encode(new GeoPoint(26.83710, 80.92060), 6);

            var d = GeoQuery.Decode(s);
            var n1 = GeoQuery.Adjacent(s,Direction.North);
            var n4 = GeoQuery.Adjacent(s, Direction.East);
            var n2 = GeoQuery.Adjacent(s, Direction.South);
            var n3 = GeoQuery.Adjacent(s, Direction.West);
            var ss= GeoQuery.Neighbours(s);

        }
    }
}
