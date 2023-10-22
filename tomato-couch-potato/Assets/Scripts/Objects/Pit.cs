using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chickenen.Heart
{
    public class Pit : MonoBehaviour
    {
        [SerializeField]
        bool constant = true;

        SpriteRenderer sr;
        Vector2 size;

        void Start()
        {
            sr = GetComponent<SpriteRenderer>();
            size = sr.bounds.size;
        }

        void Update()
        {
            if (!constant)
            {
                size = sr.bounds.size;
            }
        }
    }
}