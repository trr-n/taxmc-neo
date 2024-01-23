using UnityEngine;
using trrne.Box;
using System;

namespace trrne.Core
{
    public class Cam : MonoBehaviour
    {
        public bool Followable { get; set; }

        [SerializeField]
        float offsetY = 5;

        // (float key, float wheel) axis = (0, 0);
        struct axis { public static float key, wheel; }
        float raw = 0f;
        readonly struct ZOOM { public const float MIN = 4.5f, MAX = 16; }

        Player player;


        void Start()
        {
            player = Gobject.GetWithTag<Player>(Constant.Tags.PLAYER);
        }

        void Update()
        {
            Zoom();
        }

        void LateUpdate()
        {
            if (!Followable)
            {
                return;
            }

            var p = player.transform.position;
            transform.position = new(p.x, p.y + offsetY, -10);
        }

        void Zoom()
        {
            if (Inputs.Down(Constant.Keys.RESET_ZOOM))
            {
                FetchValue(raw = ZOOM.MAX - ZOOM.MIN);
            }

            axis.key = Input.GetAxisRaw(Constant.Keys.ZOOM);
            axis.wheel = Input.GetAxisRaw(Constant.Keys.MOUSE_ZOOM);
            if (MF.ZeroTwins(axis.key + axis.wheel))
            {
                return;
            }

            if (axis.key != 0)
            {
                raw += MathF.Sign(axis.key) * .25f;
            }
            else if (axis.wheel != 0)
            {
                raw += -MathF.Sign(axis.wheel);
            }
            FetchValue(raw = Mathf.Clamp(raw, ZOOM.MIN, ZOOM.MAX));
        }

        void FetchValue(float value) => Camera.main.orthographicSize = value;
    }
}

// https://docs.unity3d.com/ja/current/ScriptReference/Vector2.SmoothDamp.html