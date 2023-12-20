#pragma warning disable IDE1006

using UnityEngine;

namespace trrne.Box
{
    public sealed class V2
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

        public V2 get => this;

        public static V2 operator +(in V2 a) => new(+a.x, +a.y);
        public static V2 operator -(in V2 a) => new(-a.x, -a.y);

        public static V2 operator +(in V2 a, in V2 b) => new(a.x + b.x, a.y + b.y);
        public static V2 operator +(in V2 a, in float b) => new(a.x + b, a.y + b);
        public static V2 operator +(in float b, in V2 a) => new(b - a.x, b - a.y);

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

        public static explicit operator Vector2(in V2 a) => new(a.x, a.y);
        public static explicit operator Vector3(in V2 a) => new(a.x, a.y, 0f);

        public override bool Equals(object obj) => base.Equals(obj);
        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => $"({x},{y})";
        public static string ToString(in V2 a) => a.ToString();
    }
}