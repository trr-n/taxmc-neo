using trrne.Body;
using trrne.Bag;
using UnityEngine;
using UnityEngine.UI;

namespace trrne.Brain
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

            Physics2D.gravity = Vector100.gravity;

            cam = Gobject.GetWithTag<Cam>(Constant.Tags.MainCamera);
            cam.followable = true;

            player = Gobject.GetWithTag<Player>(Constant.Tags.Player);
            player.controllable = true;

            Gobject.Finds(Constant.Tags.Enemy)
                .ForEach(enemy => enemy.GetComponent<Creature>().enable = true);
        }

        void Update()
        {
            timeT.SetText(time.CurrentSTR);
        }
    }
}
