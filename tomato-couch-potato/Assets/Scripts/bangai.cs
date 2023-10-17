// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
// using Self.Utils;

namespace trrne.test
{
    public class bangai : MonoBehaviour
    {
        int a, b;
        int n = 10;

        void Start()
        {
            // for
            for (int i = 0; i <= n; i++)
            {
                a += i;
            }

            // koushiki
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
}
