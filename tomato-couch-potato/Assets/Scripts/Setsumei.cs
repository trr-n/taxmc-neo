using System;
using System.Collections;
using System.Collections.Generic;
using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class Setsumei : MonoBehaviour
    {
        void Update()
        {
            transform.SetPosition(y: MathF.Sin(Time.time * 2));
        }
    }
}
