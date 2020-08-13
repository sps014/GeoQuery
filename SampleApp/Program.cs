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
            var radius = 5000;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            _ = HashQuery.GetNearbyHashes(new GeoPoint(50, -50), radius, 8,false);

            var v1 = stopwatch.ElapsedMilliseconds;
            stopwatch.Stop();
            Console.WriteLine("radius:  " + radius + " meters");
            Console.WriteLine("Time Taken :  " + v1 + " ms");


        }
    }
}
