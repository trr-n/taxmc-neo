// #pragma warning disable IDE1006

using System;
using UnityEngine;

namespace trrne.Box
{
    public sealed class V3
    {
        public double x, y, z;

        public V3(in double x, in double y, in double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public V3() : this(0, 0, 0) { }

        public V3(in V3 other) : this(other.x, other.y, other.z) { }

        public static V3 operator +(in V3 a) => new(+a.x, +a.y, +a.z);
        public static V3 operator +(in V3 a, in V3 b) => new(a.x + b.x, a.y + b.y, a.z + b.z);
        public static V3 operator +(in V3 a, in double b) => new(a.x + b, a.y + b, a.z + b);
        public static V3 operator +(in double b, in V3 a) => new(b + a.x, b + a.y, b + a.z);

        public static V3 operator -(in V3 a) => new(-a.x, -a.y, -a.z);
        public static V3 operator -(in V3 a, in V3 b) => new(a.x - b.x, a.y - b.y, a.z - b.z);
        public static V3 operator -(in V3 a, in double b) => new(a.x - b, a.y - b, a.z - b);
        public static V3 operator -(in double b, in V3 a) => new(b - a.x, b - a.y, b - a.z);

        public static V3 operator *(in V3 a, in V3 b) => new(a.x * b.x, a.y * b.y, a.z * b.z);
        public static V3 operator *(in V3 a, in double b) => new(a.x * b, a.y * b, a.z * b);
        public static V3 operator *(in double b, in V3 a) => new(b * a.x, b * a.y, b * a.z);

        public static V3 operator /(in V3 a, in V3 b) => new(a.x / b.x, a.y / b.y, a.z / b.z);
        public static V3 operator /(in V3 a, in double b) => new(a.x / b, a.y / b, a.z / b);
        public static V3 operator /(in double a, in V3 b) => new(a / b.x, a / b.y, a / b.z);

        public static V3 operator ++(in V3 a) => new(a.x + 1, a.y + 1, a.z + 1);
        public static V3 operator --(in V3 a) => new(a.x - 1, a.y - 1, a.z - 1);

        public static bool operator ==(in V3 a, in V3 b) => a.x == b.x && a.y == b.y && a.z == b.z;
        public static bool operator !=(in V3 a, in V3 b) => a.x != b.x && a.y != b.y && a.z != b.z;
        public static bool operator >(in V3 a, in V3 b) => a.x > b.x && a.y > b.y && a.z > b.z;
        public static bool operator >=(in V3 a, in V3 b) => a.x >= b.x && a.y >= b.y && a.z >= b.z;
        public static bool operator <(in V3 a, in V3 b) => a.x < b.x && a.y < b.y && a.z < b.z;
        public static bool operator <=(in V3 a, in V3 b) => a.x <= b.x && a.y <= b.y && a.z <= b.z;

        // public static bool operator >(in V3 a, in double b) => a.x > b && a.y > b && a.z > b;
        // public static bool operator >=(in V3 a, in double b) => a.x >= b && a.y >= b && a.z >= b;
        // public static bool operator <(in V3 a, in double b) => a.x < b && a.y < b && a.z < b;
        // public static bool operator <=(in V3 a, in double b) => a.x <= b && a.y <= b && a.z <= b;

        public bool Twins(in double ofs) => ofs.Twins(x) && ofs.Twins(y) && ofs.Twins(z);

        public static explicit operator string(in V3 a) => $"({a.x}, {a.y}, {a.z})";
        public static explicit operator Vector2(in V3 a) => new((float)a.x, (float)a.y);
        public static explicit operator Vector3(in V3 a) => new((float)a.x, (float)a.y, (float)a.z);

        public override bool Equals(object obj) => base.Equals(obj);
        public override int GetHashCode() => base.GetHashCode();

        public double Mag() => Math.Sqrt(x * x + y * y);
        public V3 Nor() => new(x / Mag(), y / Mag(), z / Mag());

        public double Dot(in V3 other) => x * other.x + y * other.y + z * other.z;
        public double Dot(in V3 a, in V3 b) => a.Dot(b);

        public double Angle(in V3 other)
        {
            double lselfl = Mag(), lotherl = other.Mag();
            if (Math.Abs(lselfl + lotherl) < 1e-44)
            {
                return .0;
            }
            return Math.Acos(Dot(other) / lselfl / lotherl) * (180 / MathF.PI);
        }
        public static double Angle(in V3 a, in V3 b) => a.Angle(b);

        public void Translate(in V3 v)
        {
            if (v.Twins(0.0))
            {
                return;
            }

            x += v.x;
            y += v.y;
            z += v.z;
        }

        public void Translate(in double d)
        {
            if (d.Twins(0.0))
            {
                return;
            }

            x += d;
            y += d;
            z += d;
        }
    }
}