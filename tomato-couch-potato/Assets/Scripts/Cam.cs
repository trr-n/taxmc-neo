using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using trrne.Teeth;

namespace trrne.Body
{
    public class Cam : MonoBehaviour
    {
        public bool followable { get; set; }

        GameObject player;
        float size, zoom;
        readonly Vector3 offset = new(0, 1, -10);

        void Start()
        {
            followable = true;
            player = Gobject.GetWithTag(Constant.Tags.Player);
        }

        void Update()
        {
            Zoom();
            Follow();
        }

        void Follow()
        {
            if (followable)
            {
                transform.position = player.transform.position + offset;
            }
        }

        void Zoom()
        {
            zoom = Input.GetAxisRaw(Constant.Keys.Zoom);

            if (zoom != 0)
            {
                size = Mathf.Sign(zoom) / 4;
                Camera.main.orthographicSize += size;
                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 2.5f, 12);
            }
        }
    }
}
