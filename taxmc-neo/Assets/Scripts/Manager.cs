using System.Collections;
using System.Collections.Generic;
using Self.Utils;
using UnityEngine;

namespace Self.Game
{
    public class Manager : MonoBehaviour
    {
        struct A
        {
            public int aa;
        }

        void Start()
        {
            Physics2D.gravity = Coordinate.G;

            A a = new() { aa = 1 };
            Save.Write2(a, "a", Application.dataPath + "/aa.sav");
        }
    }
}
