using UnityEngine;
using trrne.Box;

namespace trrne.Core
{
    public class Cam : MonoBehaviour
    {
        public bool Followable { get; set; }

        Player player;

        [SerializeField]
        float offsetY = 1;

        float z;
        Vector2 refv = new();

        [SerializeField]
        Vector2 spd = new(0.01f, 1e-12f);

        float axis = 0f;

        void Start()
        {
            player = Gobject.GetWithTag<Player>(Constant.Tags.Player);
            z = transform.position.z;
        }

        void Update()
        {
            Zoom();
            // Follow();
        }

        void LateUpdate()
        {
            // Zoom();
            Follow();
        }

        void Follow()
        {
            if (!Followable)
            {
                return;
            }
            Vector2 self = transform.position,
                player = this.player.transform.position;
            float dx = Mathf.SmoothDamp(self.x, player.x, ref refv.x, spd.x),
                dy = Mathf.SmoothDamp(self.y, player.y, ref refv.y, spd.y) + offsetY;
            transform.position = new(dx, dy, z);
        }

        void Zoom()
        {

            if ((axis = Input.GetAxisRaw(Constant.Keys.Zoom)) == 0)
            {
                return;
            }
            Camera.main.orthographicSize += Mathf.Sign(axis) / 4;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 2.5f, 12);
        }
    }
}

// https://docs.unity3d.com/ja/current/ScriptReference/Vector2.SmoothDamp.html