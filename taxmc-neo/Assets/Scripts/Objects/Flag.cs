using System.Collections;
using System.Collections.Generic;
using trrne.utils;
using UnityEditor.Build.Content;
using UnityEngine;

namespace trrne.Game
{
    public class Flag : MonoBehaviour
    {
        [SerializeField]
        Sprite[] flags;

        SpriteRenderer sr;

        Vector2 bcastSize;

        void Start()
        {
            sr = GetComponent<SpriteRenderer>();
            sr.sprite = flags[0];
            bcastSize = new(sr.bounds.size.x / 2, sr.bounds.size.y);
        }

        void Update()
        {
            HitMe();
        }

        void HitMe()
        {
            if (Gobject.BoxCast2D(out _, transform.position, bcastSize, Constant.Layers.Player, 0, 0))
            {
                sr.sprite = flags[1];
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.HSVToRGB(Time.unscaledDeltaTime % 1, 1, 1);
            Gizmos.DrawWireCube(transform.position, bcastSize);
        }
    }
}
