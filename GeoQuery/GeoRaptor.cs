using System;
using System.Collections.Generic;
using System.Text;

namespace GeoQuery
{
    public static class GeoRaptor
    {
        private const string Base = "0123456789bcdefghjkmnpqrstuvwxyz";
        static HashSet<string> GetCombinations(string hash)
        {
            var combination=new HashSet<string>();

            foreach (char c in Base)
            {
                combination.Add(hash + c);
            }

            return combination;
        }
        public static HashSet<string> Compress(HashSet<string> GeoHashes,int minLevel,int maxLevel)
        {
            var deleteGeoHash = new HashSet<string>();
            var finalGeoHash = new HashSet<string>();
            var flag = true;
            var finalGeoHashSize = 0;

            if(GeoHashes==null)
            {
                throw new ArgumentNullException("GeoHashes is  null");
            }
            while(flag)
            {
                finalGeoHash.Clear();
                deleteGeoHash.Clear();
                foreach (var geohash in GeoHashes)
                {
                    var geohash_length = geohash.Length;
                    if(geohash_length>=minLevel)
                    {
                        var part = geohash.Substring(0, geohash.Length - 1);
                        if(!deleteGeoHash.Contains(part) && !deleteGeoHash.Contains(geohash))
                        {
                            var combination = GetCombinations(part);
                            if(combination.IsSubsetOf(GeoHashes))
                            {
                                finalGeoHash.Add(part);
                                deleteGeoHash.Add(part);
                            }
                            else
                            {
                                deleteGeoHash.Add(geohash);

                                if (geohash_length >= maxLevel)
                                {
                                    finalGeoHash.Add(geohash.Substring(0, maxLevel));
                                }
                                else
                                {
                                    finalGeoHash.Add(geohash);
                                }
                            }
                            if(finalGeoHashSize==finalGeoHash.Count)
                            {
                                flag = false;
                            }
                        }
                    }
                }
                finalGeoHashSize = finalGeoHash.Count;

            }
            return finalGeoHash;

        }

    }
}
