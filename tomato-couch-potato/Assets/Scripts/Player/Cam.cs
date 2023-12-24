using UnityEngine;
using trrne.Box;

namespace trrne.Core
{
    public class Cam : MonoBehaviour
    {
        public bool Followable { get; set; }

        [SerializeField]
        float offsetY = 1;

        float kaxis = 0f, maxis = 0f;
        float raw = 0f;
        const float Z = -10f;

        Player player;

        // (float MIN, float MAX) ZOOM => (4.5f, 12f);
        const float ZOOM_MIN = 4.5f;
        const float ZOOM_MAX = 12f;

        void Start()
        {
            player = Gobject.GetWithTag<Player>(Config.Tags.PLAYER);
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
            transform.position = new(p.x, p.y + offsetY, Z);
        }

        void Zoom()
        {
            if (Inputs.Down(Config.Keys.RESET_ZOOM))
            {
                FetchValue(raw = ZOOM_MAX - ZOOM_MIN);
            }

            kaxis = Input.GetAxisRaw(Config.Keys.ZOOM);
            maxis = Input.GetAxisRaw(Config.Keys.MOUSE_ZOOM);
            if (0f.Twins(kaxis + maxis))
            {
                return;
            }

            if (kaxis != 0)
            {
                raw += Mathf.Sign(kaxis) / 4;
            }
            else if (maxis != 0)
            {
                raw += -Mathf.Sign(maxis);
            }
            FetchValue(raw = Mathf.Clamp(raw, ZOOM_MIN, ZOOM_MAX));
        }

        void FetchValue(float value)
        => Camera.main.orthographicSize = value;
    }
}

// https://docs.unity3d.com/ja/current/ScriptReference/Vector2.SmoothDamp.html