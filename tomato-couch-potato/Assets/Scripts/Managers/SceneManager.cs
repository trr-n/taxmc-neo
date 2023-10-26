using trrne.Pancreas;
using UnityEngine;

namespace trrne.Brain
{
    public class SceneManager : MonoBehaviour
    {
        public void Title() => Scenes.Load(Constant.Scenes.Title);

        public void Select() => Scenes.Load(Constant.Scenes.Select);

        public void Game1() => Scenes.Load(Constant.Scenes.Game0);

        public void Game2() => Scenes.Load(Constant.Scenes.Game1);

        public void Quit() => Application.Quit();
    }
}