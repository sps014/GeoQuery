using System;
using System.Collections.Generic;
using System.Text;

namespace GeoQuery
{
    public readonly struct GeoPoint : IEquatable<GeoPoint>
    {
        public double Latitude { get; }

        public double Longitude { get; }

        public GeoPoint(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public override bool Equals(object obj) =>
                    (obj is GeoPoint data) && Equals(data);

        public bool Equals(GeoPoint other) =>
            Latitude.Equals(other.Latitude) && Longitude.Equals(other.Longitude);

        public override int GetHashCode() =>
            Latitude.GetHashCode() ^ Longitude.GetHashCode();

        public static bool operator ==(GeoPoint left, GeoPoint right) =>
            left.Equals(right);

        public static bool operator !=(GeoPoint left, GeoPoint right) =>
            !left.Equals(right);
    }

}
