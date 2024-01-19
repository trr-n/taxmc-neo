using UnityEngine;
using trrne.Box;
using System;

namespace trrne.Core
{
    public class Cam : MonoBehaviour
    {
        public bool Followable { get; set; }

        [SerializeField]
        float offsetY = 1;

        (float key, float wheel) axis = (0, 0);

        float raw = 0f;

        Player player;

        readonly (float min, float max) zoom = (4.5f, 12f);

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

            Vector3 p = player.transform.position;
            transform.position = new(p.x, p.y + offsetY, -10);
        }

        void Zoom()
        {
            if (Inputs.Down(Constant.Keys.RESET_ZOOM))
            {
                FetchValue(raw = zoom.max - zoom.min);
            }

            axis = (Input.GetAxisRaw(Constant.Keys.ZOOM), Input.GetAxisRaw(Constant.Keys.MOUSE_ZOOM));
            if (MF.ZeroTwins(axis.key + axis.wheel))
            {
                return;
            }

            if (axis.key != 0)
            {
                raw += MathF.Sign(axis.key) / 4;
            }
            else if (axis.wheel != 0)
            {
                raw += -MathF.Sign(axis.wheel);
            }
            FetchValue(raw = Mathf.Clamp(raw, zoom.min, zoom.max));
        }

        void FetchValue(float value) => Camera.main.orthographicSize = value;
    }
}

// https://docs.unity3d.com/ja/current/ScriptReference/Vector2.SmoothDamp.html