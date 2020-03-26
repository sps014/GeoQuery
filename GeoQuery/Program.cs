using System;

namespace GeoQuery
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = GeoQuery.Encode(26.83710, 80.92060, 6);
            var d = GeoQuery.Decode(s);
        }
    }
}
