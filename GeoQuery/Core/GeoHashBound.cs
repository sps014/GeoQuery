using System;
using System.Collections.Generic;
using System.Text;

namespace GeoQuery
{
    public readonly struct GeoHashBound : IEquatable<GeoHashBound>
    {
        public GeoPoint Southwest { get; }

        public GeoPoint Northeast { get; }

        public GeoHashBound(GeoPoint southWest, GeoPoint northEast)
        {
            Southwest = southWest;
            Northeast = northEast;
        }

        public override bool Equals(object obj) =>
                    (obj is GeoHashBound data) && Equals(data);

        public bool Equals(GeoHashBound other) =>
            Southwest.Equals(other.Southwest) && Northeast.Equals(other.Northeast);

        public override int GetHashCode() =>
            Southwest.GetHashCode() ^ Northeast.GetHashCode();

        public static bool operator ==(GeoHashBound left, GeoHashBound right) =>
            left.Equals(right);

        public static bool operator !=(GeoHashBound left, GeoHashBound right) =>
            !left.Equals(right);
    }

}
