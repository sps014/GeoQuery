using GeoQuery;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<string> set = new HashSet<string>();
            //StreamReader reader = new StreamReader(@"C:\Users\shive\Desktop\m.txt");
            //while(!reader.EndOfStream)
            //{
            //    set.Add(reader.ReadLine());
            //}
            //reader.Close();

            var mosfet = GeoQuery.GeoQuery.GetNearbyHashes(new GeoPoint(50, -50), 100000, 7);
            var t2 = GeoQuery.GeoQuery.CreateGeoHash(new GeoPoint(50, -50), 100000,7, true, 1, 7);
            mosfet=GeoRaptor.Compress(mosfet, 1, 7).OrderBy(x=>x.Length).ToHashSet();
        }
    }
}
