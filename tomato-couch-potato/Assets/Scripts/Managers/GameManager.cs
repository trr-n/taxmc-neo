using Chickenen.Heart;
using Chickenen.Pancreas;
using UnityEngine;
using UnityEngine.UI;

namespace Chickenen.Brain
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        Text timeT;

        Cam cam;
        Player player;
        TimeManager time;
        PauseMenu menu;

        void Start()
        {
            time = GetComponent<TimeManager>();
            time.Start();

            menu = GetComponent<PauseMenu>();
            menu.Inactive();

            Physics2D.gravity = Vector100.Gravity;

            cam = Gobject.GetWithTag<Cam>(Constant.Tags.MainCamera);
            cam.Followable = true;

            player = Gobject.GetWithTag<Player>(Constant.Tags.Player);
            player.Controllable = true;

            var enemies = Gobject.Finds(Constant.Tags.Enemy);
            enemies.ForEach(enemy => enemy.GetComponent<Creature>().Enable = true);
        }

        void Update()
        {
            timeT.SetText(time.CurrentSTR);
        }
    }
}
