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
            var hashes = HashQuery.GetNearbyHashes(new GeoPoint(50, -50), radius, 7,true);

            var v1 = stopwatch.ElapsedMilliseconds;
            stopwatch.Stop();
            Console.WriteLine("radius:  " + radius + " meters");
            Console.WriteLine("Time Taken :  " + v1 + " ms");
            Console.WriteLine("Found: " + hashes.Count);

        }
    }
}
