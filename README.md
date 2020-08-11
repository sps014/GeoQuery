# GeoQuery and Proximity Search


 ✔️ Easy to use C# API for GeoHashing.

 ✔️ Easy to use C# API for Generating Geohash around a Location.

 ✔️ GeoHash Compression to get minmized query hashes.                 

 ✔️ 6-10 times faster than it's python equivalent.               

The algorithm is based on [Gustavo Niemeyer’s geocoding system](https://en.wikipedia.org/wiki/Geohash)

#### Usage
```C#

            using GeoQuery;
            //import lib namespace

            var s = GeoHash.Encode(26.83710, 80.92060, 12);
            ///eg output="tuxmgj12sdg1
            var sp = GeoHash.Encode(new GeoPoint(26.83710, 80.92060), 6);
             ///eg output="tuxmgj

            var d = GeoHash.Decode(s);
            //d.Latitude=26.8371
            //d.Longitude=80.92060
            
            var n1 = GeoHash.GetAdjacent(s,Direction.North);
            ///eg output="tuxmgl
            var n4 = GeoHash.GetAdjacent(s, Direction.East);
            ///eg output="tuxmgg and so on for rest two
            var n2 = GeoHash.GetAdjacent(s, Direction.South);
            var n3 = GeoHash.GetAdjacent(s, Direction.West);
            
            var ss= GeoHash.GetNeighbours(s);
            //ss.North=tuxmgl
            //ss.SouthEast=tuxmg1
           // and so on
           
           //Create Geohashes around a point with hash precision of 8
           var hashes=HashQuery.GetNearbyHashes(new GeoPoint(50, -50), radius, 8);
           
           //Create Geohashes around a point with hash precision of 8 with compression
           var hashes=HashQuery.GetNearbyHashes(new GeoPoint(50, -50), radius, 8,true);
```

###### Thanks to [@chrisveness](https://github.com/chrisveness/)  port of js-lat-long lib
###### Thanks to [@ashwin711](https://github.com/ashwin711/georaptor) port of georaptor (compression)

