using GeoQuery.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace GeoQuery
{
    public class HashQuery
    {
        /// <summary>
        /// Get All the Geohashes around a point
        /// </summary>
        /// <param name="point">point around which to calculate geohashes</param>
        /// <param name="radius">Radius in meters</param>
        /// <param name="precision">geohash precision between 1-12</param>
        /// <returns></returns>
        public static HashSet<string> GetNearbyHashes(GeoPoint point,double radius,int precision,bool compress=false)
        {
            var mainHash = GeoHash.Encode(point, precision);

            var hashes=CalculateBlockNeighbour(mainHash, radius, precision);

            if (compress)
                return GeoRaptor.Compress(hashes, 1, precision);

            return hashes;

        }
        private static HashSet<string> CalculateBlockNeighbour(string hash,double radius,int precision)
        {
            var hashes = new HashSet<string>();

            Stack<(ReadOnlyMemory<char> Hash, double Distance)> StackFrame = new Stack<(ReadOnlyMemory<char> Hash, double Distance)>();

            var height = GeoBlockSize.BlockSize[precision].Height;
            var width = GeoBlockSize.BlockSize[precision].Width;
            var diagonal = MathF.Sqrt((float)((height * height) + (width * width)));

            StackFrame.Push((hash.AsMemory(), 0));

            while(StackFrame.Count>0)
            {
                var curr = StackFrame.Pop();

                if (curr.Distance > radius)
                    continue;

                hashes.Add(curr.Hash.ToString());

                var nbr = GeoHash.GetNeighbours(curr.Hash.ToString());

                if (!hashes.Contains(nbr.East))
                    StackFrame.Push((nbr.East.AsMemory(), curr.Distance + width));
                if (!hashes.Contains(nbr.West))
                    StackFrame.Push((nbr.West.AsMemory(), curr.Distance + width));
                if (!hashes.Contains(nbr.North))
                    StackFrame.Push((nbr.North.AsMemory(), curr.Distance + height));
                if (!hashes.Contains(nbr.South))
                    StackFrame.Push((nbr.South.AsMemory(), curr.Distance + height));
                if (!hashes.Contains(nbr.Northeast))
                    StackFrame.Push((nbr.Northeast.AsMemory(), curr.Distance + diagonal));
                if (!hashes.Contains(nbr.Northwest))
                    StackFrame.Push((nbr.Northwest.AsMemory(), curr.Distance + diagonal));
                if (!hashes.Contains(nbr.Southeast))
                    StackFrame.Push((nbr.Southeast.AsMemory(), curr.Distance + diagonal));
                if (!hashes.Contains(nbr.Southwest))
                    StackFrame.Push((nbr.Southwest.AsMemory(), curr.Distance + diagonal));
            }
         
            return hashes;
        }
    }
}
