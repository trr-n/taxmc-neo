using System.Collections;
using System.Collections.Generic;
using trrne.Utils;
using UnityEngine;

namespace trrne.Game
{
    public class Manager : MonoBehaviour
    {
        new Camera camera;
        Player player;
        GameObject[] enemies;

        void Start()
        {
            Physics2D.gravity = Constant.DefaultGravity;

            camera = Gobject.GetWithTag<Camera>(Constant.Tags.MainCamera);
            camera.Followable = true;

            player = Gobject.GetWithTag<Player>(Constant.Tags.Player);
            player.ctrlable = true;

            enemies = Gobject.Finds(Constant.Tags.Enemy);
            foreach (var enemy in enemies)
            {
                var en = enemy.GetComponent<Enemy>();
                en.enable = true;
            }
        }
    }
}
