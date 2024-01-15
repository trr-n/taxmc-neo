// #pragma warning disable IDE1006

using System;
using UnityEngine;

namespace trrne.Box
{
    public sealed class V2 // (double x = 0.0, double y = 0.0)
    {
        public double x, y;

        public V2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public V2() : this(0, 0) { }

        public V2 Get() => this;

        public static V2 operator +(in V2 a) => new(+a.x, +a.y);
        public static V2 operator +(in V2 a, in V2 b) => new(a.x + b.x, a.y + b.y);
        public static V2 operator +(in V2 a, in double b) => new(a.x + b, a.y + b);
        public static V2 operator +(in double b, in V2 a) => new(b + a.x, b + a.y);

        public static V2 operator -(in V2 a) => new(-a.x, -a.y);
        public static V2 operator -(in V2 a, in V2 b) => new(a.x - b.x, a.y - b.y);
        public static V2 operator -(in V2 a, in double b) => new(a.x - b, a.y - b);
        public static V2 operator -(in double b, in V2 a) => new(b - a.x, b - a.y);

        public static V2 operator *(in V2 a, in V2 b) => new(a.x * b.x, a.y * b.y);
        public static V2 operator *(in V2 a, in double b) => new(a.x * b, a.y * b);
        public static V2 operator *(in double b, in V2 a) => new(b * a.x, b * a.y);

        public static V2 operator /(in V2 a, in V2 b) => new(a.x / b.x, a.y / b.y);
        public static V2 operator /(in V2 a, in double b) => new(a.x / b, a.y / b);
        public static V2 operator /(in double a, in V2 b) => new(a / b.x, a / b.y);

        public static V2 operator ++(in V2 a) => new(a.x + 1, a.y + 1);
        public static V2 operator --(in V2 a) => new(a.x - 1, a.y - 1);

        public static bool operator ==(in V2 a, in V2 b) => a.x == b.x && a.y == b.y;
        public static bool operator !=(in V2 a, in V2 b) => a.x != b.x && a.y != b.y;
        public static bool operator >(in V2 a, in V2 b) => a.x > b.x && a.y > b.y;
        public static bool operator >=(in V2 a, in V2 b) => a.x >= b.x && a.y >= b.y;
        public static bool operator <(in V2 a, in V2 b) => a.x < b.x && a.y < b.y;
        public static bool operator <=(in V2 a, in V2 b) => a.x <= b.x && a.y <= b.y;

        public bool Twins(in double ofs) => ofs.Twins(x) && ofs.Twins(y);

        public static explicit operator string(in V2 a) => $"({a.x}, {a.y})";
        public static explicit operator Vector2(in V2 a) => new((float)a.x, (float)a.y);
        public static explicit operator Vector3(in V2 a) => new((float)a.x, (float)a.y, 0f);

        public override bool Equals(object obj) => base.Equals(obj);
        public override int GetHashCode() => base.GetHashCode();

        public double Mag() => Math.Sqrt(x * x + y * y);
        public V2 Nor() => new(x / Mag(), y / Mag());

        public double Dot(in V2 other) => x * other.x + y * other.y;
        public double Dot(in V2 a, in V2 b) => a.Dot(b);

        public double Angle(in V2 other)
        {
            double lselfl = Mag(), lotherl = other.Mag();
            if (Math.Abs(lselfl + lotherl) < 1e-44)
            {
                return .0;
            }
            return Math.Acos(Dot(other) / lselfl / lotherl) * (180 / MathF.PI);
        }
        public static double Angle(in V2 a, in V2 b) => a.Angle(b);

        public void Translate(in V2 v)
        {
            if (v.Twins(0.0))
            {
                return;
            }
            x += v.x;
            y += v.y;
        }

        public void Translate(in double d)
        {
            if (d.Twins(0.0))
            {
                return;
            }
            x += d;
            y += d;
        }
    }
}