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
            var radius = 10000;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            _ = GeoQuery.HashQuery.GetNearbyHashes(new GeoPoint(50, -50), radius, 8);
            var v1 = stopwatch.ElapsedMilliseconds;
            stopwatch.Stop();
            Console.WriteLine("radius:  " + radius + " meters");
            Console.WriteLine("Time Taken by my version of algo:  " + v1+" ms");

        }
    }
}
