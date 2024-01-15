using trrne.Core;
using trrne.Box;
using UnityEngine;
using UnityEngine.UI;

namespace trrne.Brain
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        Text timeT;

        TimeManager time;

        void Start()
        {
            time = GetComponent<TimeManager>();
            time.Start();

            PauseMenu menu = GetComponent<PauseMenu>();
            menu.Inactive();

            Physics2D.gravity = Vec.Gravity;

            Cam cam = Gobject.GetWithTag<Cam>(Constant.Tags.MAIN_CAMERA);
            cam.Followable = true;

            GameObject[] enemies = Gobject.GetsWithTag(Constant.Tags.ENEMY);
            enemies.ForEach(enemy => enemy.GetComponent<Creature>().Enable = true);

            AudioSource[] speakers = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
            speakers.ForEach(speaker => speaker.rolloffMode = AudioRolloffMode.Linear);
        }

        void Update()
        {
            timeT.SetText(time.CurrentTimeStr);
        }
    }
}
