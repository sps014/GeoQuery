using GeoQuery;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            _ = GeoQuery.GeoQuery.GetNearbyHashes(new GeoPoint(50, -50), 40000, 8);
            var v1 = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();
            _ = GeoQuery.GeoQuery.CreateGeoHash(new GeoPoint(50, -50), 40000, 8,false,1,8);
            var v2 = stopwatch.ElapsedMilliseconds;
            Console.WriteLine("Time Taken by my version of algo:  " + v1+" ms");
            Console.WriteLine("Time Taken by python counter part version of algo : " + v2+" ms");

        }
    }
}
