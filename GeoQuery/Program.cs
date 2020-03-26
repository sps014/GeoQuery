using System;

namespace GeoQuery
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = GeoQuery.Encode(new GeoPoint(125.676,18.8),12);
            var d = GeoQuery.Decode(s);
        }
    }
}
