using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using trrne.Utils;
using UCamera = UnityEngine.Camera;

namespace trrne.Game
{
    public class Cam : MonoBehaviour
    {
        public bool followable { get; set; }

        GameObject player;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag(Constant.Tags.Player);
        }

        void Update()
        {
            // if (!Followable)
            //     return;
            transform.position = player.transform.position + -Coordinate.y * 0.25f + Coordinate.z * -10;
        }
    }
}
