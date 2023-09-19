using System.Collections;
using System.Collections.Generic;
using trrne.Utils;
using UnityEngine;

namespace trrne.Game
{
    public class Manager : MonoBehaviour
    {
        new Cam camera;
        Player player;
        GameObject[] enemies;

        void Start()
        {
            Physics2D.gravity = Constant.Gravity;

            camera = Gobject.GetWithTag<Cam>(Constant.Tags.MainCamera);
            camera.followable = true;

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
