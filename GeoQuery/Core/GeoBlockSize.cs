using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace GeoQuery.Core
{
    public static class GeoBlockSize
    {
        /// <summary>
        /// Size Width Height In Meters
        /// </summary>
        public static IReadOnlyDictionary<int, (double Width, double Height)> BlockSize { get; } = new Dictionary<int, (double Width, double Height)>
        {
            {1,(5000000,5000000) },
            {2,(1250000,625000) },
            {3,(156000,156000) },
            {4,(39100,19500) },
            {5,(4890,4890) },
            {6,(1220,610) },
            {7,(153,153) },
            {8,(38.2,19.1) },
            {9,(4.77,4.77) },
            {10,(1.19,0.596) },
            {11,(0.149,0.149) },
            {12,(0.0372,0.0186) }
        };
    }
}
