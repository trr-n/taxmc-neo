// #pragma warning disable IDE1006

using System;
using UnityEngine;

namespace trrne.Box
{
    public sealed class V3
    {
        public double x, y, z;

        public V3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public V3() : this(0, 0, 0) { }

        public void Set(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void Set(V3 other)
        {
            x = other.x;
            y = other.y;
            z = other.z;
        }

        public V3 Get() => this;

        public static V3 operator +(V3 a) => new(+a.x, +a.y, +a.z);
        public static V3 operator +(V3 a, V3 b) => new(a.x + b.x, a.y + b.y, a.z + b.z);
        public static V3 operator +(V3 a, double b) => new(a.x + b, a.y + b, a.z + b);
        public static V3 operator +(double b, V3 a) => new(b + a.x, b + a.y, b + a.z);

        public static V3 operator -(V3 a) => new(-a.x, -a.y, -a.z);
        public static V3 operator -(V3 a, V3 b) => new(a.x - b.x, a.y - b.y, a.z - b.z);
        public static V3 operator -(V3 a, double b) => new(a.x - b, a.y - b, a.z - b);
        public static V3 operator -(double b, V3 a) => new(b - a.x, b - a.y, b - a.z);

        public static V3 operator *(V3 a, V3 b) => new(a.x * b.x, a.y * b.y, a.z * b.z);
        public static V3 operator *(V3 a, double b) => new(a.x * b, a.y * b, a.z * b);
        public static V3 operator *(double b, V3 a) => new(b * a.x, b * a.y, b * a.z);

        public static V3 operator /(V3 a, V3 b) => new(a.x / b.x, a.y / b.y, a.z / b.z);
        public static V3 operator /(V3 a, double b) => new(a.x / b, a.y / b, a.z / b);
        public static V3 operator /(double a, V3 b) => new(a / b.x, a / b.y, a / b.z);

        public static V3 operator ++(V3 a) => new(a.x + 1, a.y + 1, a.z + 1);
        public static V3 operator --(V3 a) => new(a.x - 1, a.y - 1, a.z - 1);

        public static bool operator ==(V3 a, V3 b) => a.x == b.x && a.y == b.y && a.z == b.z;
        public static bool operator !=(V3 a, V3 b) => a.x != b.x && a.y != b.y && a.z != b.z;
        public static bool operator >(V3 a, V3 b) => a.x > b.x && a.y > b.y && a.z > b.z;
        public static bool operator >=(V3 a, V3 b) => a.x >= b.x && a.y >= b.y && a.z >= b.z;
        public static bool operator <(V3 a, V3 b) => a.x < b.x && a.y < b.y && a.z < b.z;
        public static bool operator <=(V3 a, V3 b) => a.x <= b.x && a.y <= b.y && a.z <= b.z;

        public static explicit operator string(V3 a) => $"({a.x}, {a.y}, {a.z})";
        public static explicit operator Vector2(V3 a) => new((float)a.x, (float)a.y);
        public static explicit operator Vector3(V3 a) => new((float)a.x, (float)a.y, (float)a.z);

        public override bool Equals(object obj) => base.Equals(obj);
        public override int GetHashCode() => base.GetHashCode();

        public double Mag() => Math.Sqrt(x * x + y * y);
        public V3 Nor() => new(x / Mag(), y / Mag(), z / Mag());

        public double Dot(V3 other) => x * other.x + y * other.y + z * other.z;
        public double Dot(V3 a, V3 b) => a.Dot(b);

        public double Angle(V3 other)
        {
            double lselfl = Mag(), lotherl = other.Mag();
            if (Math.Abs(lselfl + lotherl) < 1e-44)
            {
                return .0;
            }
            return Math.Acos(Dot(other) / lselfl / lotherl) * (180 / MathF.PI);
        }
        public static double Angle(V3 a, V3 b) => a.Angle(b);
    }
}