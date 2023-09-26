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

            Physics2D.gravity = Coordinate.gravity;

            cam = Gobject.GetWithTag<Cam>(Fixed.Tags.MainCamera);
            cam.followable = true;

            player = Gobject.GetWithTag<Player>(Fixed.Tags.Player);
            player.ctrlable = true;

            var enemies = Gobject.Finds(Fixed.Tags.Enemy);
            foreach (var enemy in enemies)
            {
                var en = enemy.GetComponent<Enemy>();
                en.enable = true;
            }
        }

        void Update()
        {
            timeT.SetText(Numeric.Round(time.current, 1));
        }
    }
}
