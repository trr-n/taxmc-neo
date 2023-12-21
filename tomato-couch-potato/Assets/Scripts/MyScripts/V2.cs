// #pragma warning disable IDE1006

using System;
using UnityEngine;

namespace trrne.Box
{
    public sealed class V2 // : IV<V2>
    {
        public double x, y;

        public V2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public V2() : this(0, 0) { }

        public void Set(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public void Set(V2 other)
        {
            x = other.x;
            y = other.y;
        }

        public V2 Get() => this;

        public static V2 operator +(V2 a) => new(+a.x, +a.y);
        public static V2 operator +(V2 a, V2 b) => new(a.x + b.x, a.y + b.y);
        public static V2 operator +(V2 a, double b) => new(a.x + b, a.y + b);
        public static V2 operator +(double b, V2 a) => new(b + a.x, b + a.y);

        public static V2 operator -(V2 a) => new(-a.x, -a.y);
        public static V2 operator -(V2 a, V2 b) => new(a.x - b.x, a.y - b.y);
        public static V2 operator -(V2 a, double b) => new(a.x - b, a.y - b);
        public static V2 operator -(double b, V2 a) => new(b - a.x, b - a.y);

        public static V2 operator *(V2 a, V2 b) => new(a.x * b.x, a.y * b.y);
        public static V2 operator *(V2 a, double b) => new(a.x * b, a.y * b);
        public static V2 operator *(double b, V2 a) => new(b * a.x, b * a.y);

        public static V2 operator /(V2 a, V2 b) => new(a.x / b.x, a.y / b.y);
        public static V2 operator /(V2 a, double b) => new(a.x / b, a.y / b);
        public static V2 operator /(double a, V2 b) => new(a / b.x, a / b.y);

        public static V2 operator ++(V2 a) => new(a.x + 1, a.y + 1);
        public static V2 operator --(V2 a) => new(a.x - 1, a.y - 1);

        public static bool operator ==(V2 a, V2 b) => a.x == b.x && a.y == b.y;
        public static bool operator !=(V2 a, V2 b) => a.x != b.x && a.y != b.y;
        public static bool operator >(V2 a, V2 b) => a.x > b.x && a.y > b.y;
        public static bool operator >=(V2 a, V2 b) => a.x >= b.x && a.y >= b.y;
        public static bool operator <(V2 a, V2 b) => a.x < b.x && a.y < b.y;
        public static bool operator <=(V2 a, V2 b) => a.x <= b.x && a.y <= b.y;

        public static explicit operator string(V2 a) => $"({a.x}, {a.y})";
        public static explicit operator Vector2(V2 a) => new((float)a.x, (float)a.y);
        public static explicit operator Vector3(V2 a) => new((float)a.x, (float)a.y, 0f);

        public override bool Equals(object obj) => base.Equals(obj);
        public override int GetHashCode() => base.GetHashCode();

        public double Mag() => Math.Sqrt(x * x + y * y);
        public V2 Nor() => new(x / Mag(), y / Mag());

        public double Dot(V2 other) => x * other.x + y * other.y;
        public double Dot(V2 a, V2 b) => a.Dot(b);
        // double IV<V2>.Dot(V2 a, V2 b) => a.Dot(b);

        public double Angle(V2 other)
        {
            double lselfl = Mag(), lotherl = other.Mag();
            if (Math.Abs(lselfl + lotherl) < 1e-44)
            {
                return .0;
            }
            return Math.Acos(Dot(other) / lselfl / lotherl) * (180 / MathF.PI);
        }
        public static double Angle(V2 a, V2 b) => a.Angle(b);
        // double IV<V2>.Angle(V2 a, V2 b) => .0f;
    }
}