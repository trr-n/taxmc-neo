using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using trrne.Bag;

namespace trrne.Body
{
    public class Cam : MonoBehaviour
    {
        public bool Followable { get; set; } = true;

        GameObject player;

        float size;
        float zoom;

        readonly Vector3 offset = new(0, -0.25f, -10);

        void Start()
        {
            player = Gobject.GetWithTag(Constant.Tags.Player);
        }

        void Update()
        {
            Zoom();

            if (!Followable) { return; }

            transform.position = player.transform.position + offset;
        }

        void Zoom()
        {
            if ((zoom = Input.GetAxisRaw(Constant.Keys.Zoom)) == 0) { return; }

            size = Mathf.Sign(zoom) / 4;
            Camera.main.orthographicSize += size;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 2.5f, 12);
        }
    }
}
