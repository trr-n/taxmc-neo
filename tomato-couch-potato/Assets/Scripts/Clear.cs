using UnityEngine;
using trrne.Brain;
using System;

namespace trrne.Core
{
    public class Clear : MonoBehaviour
    {
        [SerializeField]
        [Header("0: down\n1: up")]
        Sprite[] sprites;

        SpriteRenderer sr;

        void Start()
        {
            (sr = GetComponent<SpriteRenderer>()).sprite = sprites[0];
        }

        void OnTriggerEnter2D(Collider2D info)
        {
            if (info.TryGetComponent(out Player _))
            {
                sr.sprite = sprites[1];
                try { Recorder.Instance.Clear(); }
                catch (Exception) { print("Cannot read data of singleton."); }
            }
        }
    }
}
