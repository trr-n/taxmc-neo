using System.Collections;
using System.Collections.Generic;
using trrne.Bag;
using UnityEngine;

namespace trrne.Body
{
    public class PadCore : MonoBehaviour
    {
        [SerializeField]
        [Range(0f, 15)]
        float power = 0.1f;
        public float Power => power;
    }
}
