using System;
using trrne.Box;
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

    public class V2
    {
        float x, y;

        public V2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public V2() : this(0f, 0f) { }

        public static V2 operator +(V2 a, V2 b) => new(a.x + b.x, a.y + b.y);
        public static V2 operator +(V2 a, float b) => new(a.x + b, a.y + b);
        public static V2 operator +(float a, V2 b) => new(a + b.x, a + b.y);

        public static V2 operator -(V2 a, V2 b) => new(a.x - b.x, a.y - b.y);
        public static V2 operator -(V2 a, float b) => new(a.x - b, a.y - b);
        public static V2 operator -(float a, V2 b) => new(a - b.x, a - b.y);

        public static V2 operator *(V2 a, V2 b) => new(a.x * b.x, a.y * b.y);
        public static V2 operator *(V2 a, float b) => new(a.x * b, a.y * b);
        public static V2 operator *(float a, V2 b) => new(a * b.x, a * b.y);

        public static V2 operator /(V2 a, V2 b) => new(a.x / b.x, a.y / b.y);
        public static V2 operator /(V2 a, float b) => new(a.x / b, a.y / b);
        public static V2 operator /(float a, V2 b) => new(a / b.x, a / b.y);

        public static bool operator ==(V2 a, V2 b) => a.x == b.x && a.y == b.y;
        public static bool operator !=(V2 a, V2 b) => a.x != b.x && a.y != b.y;

        public static bool operator >(V2 a, V2 b) => a.x > b.x && a.y > b.y;
        public static bool operator >=(V2 a, V2 b) => a.x >= b.x && a.y >= b.y;

        public static bool operator <(V2 a, V2 b) => a.x < b.x && a.y < b.y;
        public static bool operator <=(V2 a, V2 b) => a.x <= b.x && a.y <= b.y;

        public V2 Abs() => new(x >= 0 ? x : -x, y >= 0 ? y : -y);

        public static float Dot(V2 a, V2 b) => a.x * b.x + a.y * b.y;

        public static float Cross(V2 a, V2 b) => a.x * b.y - a.y * b.x;

        public float Magnitude() => MathF.Sqrt(x * x + y * y);

        public static float Distance(V2 a, V2 b) => (a - b).Magnitude();

        public static float Angle(V2 a, V2 b)
        {
            float na = a.Magnitude(), nb = b.Magnitude();
            if (MathF.Abs(na + nb) < 1e-45f)
            {
                return 0f;
            }
            return MathF.Acos(Dot(a, b) / na / nb * (180 / MathF.PI));
        }

        public static V2 FromPolar(V2 a) => new(a.x * MathF.Cos(a.y), a.x * MathF.Sin(a.y));
    }
}
