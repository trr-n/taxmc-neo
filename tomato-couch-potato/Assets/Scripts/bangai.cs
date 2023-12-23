using System;
using UnityEngine;

namespace trrne.test
{
    public class bangai : MonoBehaviour
    {
        int a, b;
        readonly int n = 10;

        void Start()
        {
            for (int i = 0; i <= n; i++)
            {
                a += i;
            }

            // ■■■■■■■■■■
            // ■■■■■■■■■▢
            // ■■■■■■■■▢▢
            // ■■■■■■■▢▢▢
            // ■■■■■■▢▢▢▢
            // ■■■■■▢▢▢▢▢
            // ■■■■▢▢▢▢▢▢
            // ■■■▢▢▢▢▢▢▢
            // ■■▢▢▢▢▢▢▢▢
            // ■▢▢▢▢▢▢▢▢▢
            // ▢▢▢▢▢▢▢▢▢▢
            b = n * (n + 1) / 2;

            print($"1..{n}\nfor: {a}\nkoushiki: {b}");
        }
    }

    public class V2 : IEquatable<V2>
    {
        private double x, y;

        public V2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public V2() : this(0f, 0f) { }

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

        public V2 Get() => new(x, y);

        public static V2 operator +(V2 a, V2 b) => new(a.x + b.x, a.y + b.y);
        public static V2 operator +(V2 a, double b) => new(a.x + b, a.y + b);
        public static V2 operator +(double a, V2 b) => new(a + b.x, a + b.y);

        public static V2 operator -(V2 a, V2 b) => new(a.x - b.x, a.y - b.y);
        public static V2 operator -(V2 a, double b) => new(a.x - b, a.y - b);
        public static V2 operator -(double a, V2 b) => new(a - b.x, a - b.y);

        public static V2 operator *(V2 a, V2 b) => new(a.x * b.x, a.y * b.y);
        public static V2 operator *(V2 a, double b) => new(a.x * b, a.y * b);
        public static V2 operator *(double a, V2 b) => new(a * b.x, a * b.y);

        public static V2 operator /(V2 a, V2 b) => new(a.x / b.x, a.y / b.y);
        public static V2 operator /(V2 a, double b) => new(a.x / b, a.y / b);
        public static V2 operator /(double a, V2 b) => new(a / b.x, a / b.y);

        public static bool operator ==(V2 a, V2 b) => a.x == b.x && a.y == b.y;
        public static bool operator !=(V2 a, V2 b) => a.x != b.x && a.y != b.y;

        public static bool operator >(V2 a, V2 b) => a.x > b.x && a.y > b.y;
        public static bool operator >=(V2 a, V2 b) => a.x >= b.x && a.y >= b.y;

        public static bool operator <(V2 a, V2 b) => a.x < b.x && a.y < b.y;
        public static bool operator <=(V2 a, V2 b) => a.x <= b.x && a.y <= b.y;

        // public bool Equals(V2 other) => new V2(x, y) == other;
        public bool Equals(V2 other) => this == other;

        public override bool Equals(object obj) => base.Equals(obj);
        public override int GetHashCode() => base.GetHashCode();

        public V2 Abs() => new(Math.Abs(x), Math.Abs(y));

        public static double Dot(V2 a, V2 b) => a.x * b.x + a.y * b.y;

        public static double Cross(V2 a, V2 b) => a.x * b.y - a.y * b.x;

        public double Magnitude() => Math.Sqrt(x * x + y * y);

        public static double Distance(V2 a, V2 b) => (a - b).Magnitude();

        public static double Angle(V2 a, V2 b)
        {
            double na = a.Magnitude(),
                nb = b.Magnitude();
            if (Math.Abs(na + nb) < 1e-44f)
            {
                return 0f;
            }
            return Math.Acos(Dot(a, b) / na / nb * (180 / Math.PI));
        }

        public static V2 P2R(V2 a) => new(a.x * Math.Cos(a.y), a.x * Math.Sin(a.y));
        // https://www.optics-words.com/english_for_science/coordinate.html#chapter2_3
    }
}
