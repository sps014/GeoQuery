# GeoQuery

Easy to use C# API for GeoHashing ✔️.

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
            
            var n1 = GeoHash.Adjacent(s,Direction.North);
            ///eg output="tuxmgl
            var n4 = GeoHash.Adjacent(s, Direction.East);
            ///eg output="tuxmgg and so on for rest two
            var n2 = GeoHash.Adjacent(s, Direction.South);
            var n3 = GeoHash.Adjacent(s, Direction.West);
            
            var ss= GeoHash.Neighbours(s);
            //ss.North=tuxmgl
            //ss.SouthEast=tuxmg1
            and so on
```

###### Thanks to [@chrisveness](https://github.com/chrisveness/) port of js-lat-long lib

