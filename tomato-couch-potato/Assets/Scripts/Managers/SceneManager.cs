using trrne.Box;
using UnityEngine;

namespace trrne.Brain
{
    public class SceneManager : MonoBehaviour
    {
        public void Title() => Scenes.Load(Config.Scenes.TITLE);
        public void Select() => Scenes.Load(Config.Scenes.SELECT);
        public void Game0() => Scenes.Load(Config.Scenes.GAME0);
        public void Game1() => Scenes.Load(Config.Scenes.GAME1);
        public void Quit() => Application.Quit();
    }
}