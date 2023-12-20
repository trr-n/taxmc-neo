// #pragma warning disable IDE1006

using System;
using UnityEngine;

namespace trrne.Box
{
    // public interface IV<V>
    // {
    //     V Get();
    //     float Mag();
    //     V Nor();
    //     float Dot(in V other);
    //     float Dot(in V a, in V b);
    //     float Angle(in V other);
    //     float Angle(in V a, in V b);
    // }

    public sealed class V2 // : IV<V2>
    {
        public float x, y;

        public V2(in float x, in float y)
        {
            this.x = x;
            this.y = y;
        }

        public V2() : this(0, 0) { }

        public void Set(in float x, in float y)
        {
            this.x = x;
            this.y = y;
        }

        public void Set(in V2 a)
        {
            x = a.x;
            y = a.y;
        }

        public V2 Get() => this;

        public static V2 operator +(in V2 a) => new(+a.x, +a.y);
        public static V2 operator +(in V2 a, in V2 b) => new(a.x + b.x, a.y + b.y);
        public static V2 operator +(in V2 a, in float b) => new(a.x + b, a.y + b);
        public static V2 operator +(in float b, in V2 a) => new(b - a.x, b - a.y);

        public static V2 operator -(in V2 a) => new(-a.x, -a.y);
        public static V2 operator -(in V2 a, in V2 b) => new(a.x - b.x, a.y - b.y);
        public static V2 operator -(in V2 a, in float b) => new(a.x - b, a.y - b);
        public static V2 operator -(in float b, in V2 a) => new(b - a.x, b - a.y);

        public static V2 operator *(in V2 a, in V2 b) => new(a.x * b.x, a.y * b.y);
        public static V2 operator *(in V2 a, in float b) => new(a.x * b, a.y * b);
        public static V2 operator *(in float b, in V2 a) => new(b * a.x, b * a.y);

        public static V2 operator /(in V2 a, in V2 b) => new(a.x / b.x, a.y / b.y);
        public static V2 operator /(in V2 a, in float b) => new(a.x / b, a.y / b);
        public static V2 operator /(in float a, in V2 b) => new(a / b.x, a / b.y);

        public static V2 operator ++(in V2 a) => new(a.x + 1, a.y + 1);
        public static V2 operator --(in V2 a) => new(a.x + 1, a.y + 1);

        public static bool operator ==(in V2 a, in V2 b) => a.x == b.x && a.y == b.y;
        public static bool operator !=(in V2 a, in V2 b) => a.x != b.x && a.y != b.y;
        public static bool operator >(in V2 a, in V2 b) => a.x > b.x && a.y > b.y;
        public static bool operator >=(in V2 a, in V2 b) => a.x >= b.x && a.y >= b.y;
        public static bool operator <(in V2 a, in V2 b) => a.x < b.x && a.y < b.y;
        public static bool operator <=(in V2 a, in V2 b) => a.x <= b.x && a.y <= b.y;

        public static explicit operator string(in V2 a) => $"({a.x}, {a.y})";
        public static explicit operator Vector2(in V2 a) => new(a.x, a.y);
        public static explicit operator Vector3(in V2 a) => new(a.x, a.y, 0f);

        public override bool Equals(object obj) => base.Equals(obj);
        public override int GetHashCode() => base.GetHashCode();

        public float Mag() => MathF.Sqrt(x * x + y * y);
        public V2 Nor() => new(x / Mag(), y / Mag());

        public float Dot(in V2 other) => x * other.x + y * other.y;
        public float Dot(in V2 a, in V2 b) => a.Dot(b);
        // float IV<V2>.Dot(in V2 a, in V2 b) => a.Dot(b);

        public float Angle(in V2 other)
        {
            float lselfl = Mag(), lotherl = other.Mag();
            if (MathF.Abs(lselfl + lotherl) < 1e-44)
            {
                return .0f;
            }
            return MathF.Acos(Dot(other) / lselfl / lotherl) * (180 / MathF.PI);
        }
        public static float Angle(in V2 a, in V2 b) => a.Angle(b);
        // float IV<V2>.Angle(in V2 a, in V2 b) => .0f;
    }
}