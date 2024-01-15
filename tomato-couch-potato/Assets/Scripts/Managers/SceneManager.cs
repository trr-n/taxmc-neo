using trrne.Box;
using UnityEngine;

namespace trrne.Brain
{
    public class SceneManager : MonoBehaviour
    {
        public void Title() => Scenes.Load(Constant.Scenes.TITLE);
        public void Select() => Scenes.Load(Constant.Scenes.SELECT);
        public void Game0() => Scenes.Load(Constant.Scenes.GAME0);
        public void Game1() => Scenes.Load(Constant.Scenes.GAME1);
        public void Quit() => Application.Quit();
    }
}