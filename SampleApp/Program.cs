﻿using GeoQuery;
using System;
using System.Collections.Generic;
using System.IO;

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

            var mosfet = GeoQuery.GeoQuery.GetNearbyHashes(new GeoPoint(50, -50), 8, 9);
        }
    }
}
