using UnityEngine;
using trrne.Box;

namespace trrne.Core
{
    public class Cam : MonoBehaviour
    {
        public bool Followable { get; set; }

        GameObject player;
        readonly Vector3 offset = new(0, 1, -10);
        float size, axis;

        void Start()
        {
            Followable = true;
            player = Gobject.GetWithTag(Constant.Tags.Player);
        }

        void Update()
        {
            Zoom();
            Follow();
        }

        void Follow()
        {
            if (!Followable)
            {
                return;
            }

            transform.position = player.transform.position + offset;
        }

        void Zoom()
        {
            if ((axis = Input.GetAxisRaw(Constant.Keys.Zoom)) == 0)
            {
                return;
            }

            size = Mathf.Sign(axis) / 4;
            Camera.main.orthographicSize += size;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 2.5f, 12);
        }
    }
}
