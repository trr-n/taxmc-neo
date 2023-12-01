using trrne.Box;
using UnityEngine;

namespace trrne.Brain
{
    public class SceneManager : MonoBehaviour
    {
        public void Title() => Scenes.Load(Config.Scenes.Title);
        public void Select() => Scenes.Load(Config.Scenes.Select);
        public void Game0() => Scenes.Load(Config.Scenes.Game0);
        public void Game1() => Scenes.Load(Config.Scenes.Game1);
        public void Quit() => Application.Quit();
    }
}