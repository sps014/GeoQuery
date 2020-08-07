using System;
using System.Collections.Generic;
using System.Text;

namespace GeoQuery
{
    public readonly struct Neighbour : IEquatable<Neighbour>
    {
        public string North { get; }

        public string South { get; }

        public string East { get; }

        public string West { get; }

        public string Northeast { get; }

        public string Southeast { get; }

        public string Northwest { get; }

        public string Southwest { get; }

        public Neighbour(string n, string s, string e, string w, string ne, string se, string nw, string sw)
        {
            North = n;
            South = s;
            East = e;
            West = w;
            Northeast = ne;
            Southeast = se;
            Northwest = nw;
            Southwest = sw;
        }

        public override bool Equals(object obj) =>
               (obj is Neighbour data) && Equals(data);

        public bool Equals(Neighbour other) =>
            (South ?? string.Empty).Equals(other.South) && (North ?? string.Empty).Equals(other.North)
            && (East ?? string.Empty).Equals(other.East) && (West ?? string.Empty).Equals(other.West);

        public override int GetHashCode() =>
            (West ?? string.Empty).GetHashCode() ^ (East ?? string.Empty).GetHashCode()
            + (North ?? string.Empty).GetHashCode() ^ (South ?? string.Empty).GetHashCode();

        public static bool operator ==(Neighbour left, Neighbour right) =>
            left.Equals(right);

        public static bool operator !=(Neighbour left, Neighbour right) =>
            !left.Equals(right);
    }

}
