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
        float axis = 0f;

        void Start()
        {
            player = Gobject.GetWithTag<Player>(Config.Tags.Player);
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

            var p = player.transform.position;
            transform.position = new(p.x, p.y + offsetY, z);
        }

        void Zoom()
        {
            if ((axis = Input.GetAxisRaw(Config.Keys.Zoom)) == 0)
            {
                return;
            }

            Camera.main.orthographicSize += Mathf.Sign(axis) / 4;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 2.5f, 12);
        }
    }
}

// https://docs.unity3d.com/ja/current/ScriptReference/Vector2.SmoothDamp.html