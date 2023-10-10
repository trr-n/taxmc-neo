using trrne.Bag;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace trrne.Body
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        Text timeT;

        Cam cam;
        Player player;
        TimeManager time;

        void Start()
        {
            SceneManager.UnloadSceneAsync(Constant.Scenes.StageSelect);

            time = GetComponent<TimeManager>();
            time.Start();

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
