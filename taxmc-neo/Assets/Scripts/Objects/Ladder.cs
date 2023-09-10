using System.Collections;
using System.Collections.Generic;
using Self.Utils;
using UnityEngine;

namespace Self.Game
{
    public class Ladder : MonoBehaviour
    {
        SpriteRenderer sr;

        void Start()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            DetectPlayer();
        }

        void DetectPlayer()
        {
            // if (Gobject.BoxCast2D(out var hit, transform.position, sr.bounds.size, Constant.Layers.Player))
            // {
            //     print("my boxcast: " + hit.collider.name);
            // }

            // var hit2 = Physics2D.BoxCast(transform.position, sr.bounds.size, 0, Vector2.up, 1, Constant.Layers.Player);
            // if (hit2)
            // {
            //     print("unity's boxcast: " + hit.collider.name);
            // }

            // var hit = Physics2D.Raycast(transform.position, Vector2.up, sr.bounds.size.y / 2, Constant.Layers.Player);
            // if (hit)
            // {
            //     print("unity's raycast: " + hit.collider.name);
            // }

            // if (Gobject.Raycast2D(out var hit2, transform.position, Vector2.up, Constant.Layers.Player))
            // {
            //     print("my raycast: " + hit2.collider.name);
            // }
        }
    }
}
