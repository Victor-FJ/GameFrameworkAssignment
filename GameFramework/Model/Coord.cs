using System;

namespace GameFramework.Model
{
    public readonly struct Coord : IEquatable<Coord>
    {
        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }
        public Coord((int x, int y) coord)
        {
            X = coord.x;
            Y = coord.y;
        }

        public int X { get; }
        public int Y { get; }

        public Coord AddX(int x)
        {
            return new Coord(X + x, Y);
        }
        public Coord AddY(int y)
        {
            return new Coord(X, Y + y);
        }

        public Coord Absolute() => new Coord(Math.Abs(X), Math.Abs(Y));
        public Coord DistanceCoord(Coord coord) => (this - coord).Absolute();
        public double Distance(Coord coord)
        {
            Coord distance = DistanceCoord(coord);
            return Math.Sqrt(Math.Pow(distance.X, 2) + Math.Pow(distance.Y, 2));
        }

        #region Operators
        public static Coord operator +(Coord c1, Coord c2) => new Coord(c1.X + c2.X, c1.Y + c2.Y);
        public static Coord operator -(Coord c1, Coord c2) => new Coord(c1.X - c2.X, c1.Y - c2.Y);

        public static bool operator ==(Coord c1, Coord c2) => c1.Equals(c2);
        public static bool operator !=(Coord c1, Coord c2) => !c1.Equals(c2);

        public bool Equals(Coord other) => X == other.X && Y == other.Y;
        public override bool Equals(object obj) => obj is Coord other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(X, Y);

        public override string ToString() => $"[{X}, {Y}]";

        #endregion
    }
}