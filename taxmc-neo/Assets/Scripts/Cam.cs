using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using trrne.Bag;

namespace trrne.Body
{
    public class Cam : MonoBehaviour
    {
        public bool followable { get; set; } = true;

        GameObject player;

        float size;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag(Fixed.Tags.Player);
        }

        void Update()
        {
            Zoom();

            if (!followable) { return; }

            transform.position = player.transform.position + -Coordinate.y * 0.25f + Coordinate.z * -10;
        }

        void Zoom()
        {
            float zoom = Input.GetAxisRaw(Fixed.Keys.Zoom);

            if (zoom == 0) { return; }

            size = Mathf.Sign(zoom) / 4;
            Camera.main.orthographicSize += size;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 2.5f, 12);
        }
    }
}
