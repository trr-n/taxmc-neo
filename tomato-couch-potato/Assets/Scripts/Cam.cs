using UnityEngine;
using Chickenen.Pancreas;

namespace Chickenen.Heart
{
    public class Cam : MonoBehaviour
    {
        public bool Followable { get; set; }

        GameObject player;
        readonly Vector3 offset = new(0, 1, -10);
        float size;
        float axis;

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
            if (Followable)
            {
                transform.position = player.transform.position + offset;
            }
        }

        void Zoom()
        {
            axis = Input.GetAxisRaw(Constant.Keys.Zoom);

            if (axis != 0)
            {
                size = Mathf.Sign(axis) / 4;
                Camera.main.orthographicSize += size;
                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 2.5f, 12);
            }
        }
    }
}
