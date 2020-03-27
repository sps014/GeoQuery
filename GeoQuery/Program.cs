using System;
using  GeoQuery;

namespace GC
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = GeoHash.Encode(26.83710, 80.92060, 12);
            var sp = GeoHash.Encode(new GeoPoint(26.83710, 80.92060), 6);

            var d = GeoHash.Decode(s);
            var n1 = GeoHash.Adjacent(s,Direction.North);
            var n4 = GeoHash.Adjacent(s, Direction.East);
            var n2 = GeoHash.Adjacent(s, Direction.South);
            var n3 = GeoHash.Adjacent(s, Direction.West);
            var ss= GeoHash.Neighbours(s);

        }
    }
}
