using trrne.Teeth;
using UnityEngine;

namespace trrne.Brain
{
    public class SceneManager : MonoBehaviour
    {
        public void Game0()
        {
            Scenes.Load(Constant.Scenes.Game0);
        }

        public void Game1()
        {
            Scenes.Load(Constant.Scenes.Game1);
        }

        public void Game2()
        {
            Scenes.Load(Constant.Scenes.Game2);
        }
    }
}