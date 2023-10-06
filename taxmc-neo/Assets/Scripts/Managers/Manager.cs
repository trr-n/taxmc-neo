using trrne.Bag;
using UnityEngine;
using UnityEngine.UI;

namespace trrne.Body
{
    public class Manager : MonoBehaviour
    {
        [SerializeField]
        Text timeT;

        Cam cam;
        Player player;
        TimeManager time;

        void Start()
        {
            time = GetComponent<TimeManager>();
            time.Start();

            Physics2D.gravity = Vector100.gravity;

            cam = Gobject.GetWithTag<Cam>(Constant.Tags.MainCamera);
            cam.followable = true;

            player = Gobject.GetWithTag<Player>(Constant.Tags.Player);
            player.ctrlable = true;

            Gobject.Finds(Constant.Tags.Enemy)
                .ForEach(enemy => enemy.GetComponent<Enemy>().enable = true);
        }

        void Update()
        {
            timeT.SetText(time.CurrentSTR);
        }
    }
}
