using trrne.Box;
using UnityEngine;

namespace trrne.Brain
{
    public class SceneManager : MonoBehaviour
    {
        public static void Title() => Scenes.Load(Constant.Scenes.TITLE);
        public static void Select() => Scenes.Load(Constant.Scenes.SELECT);
        public static void Game0() => Scenes.Load(Constant.Scenes.GAME0);
        public static void Game1() => Scenes.Load(Constant.Scenes.GAME1);
        public static void Quit() => Application.Quit();
    }
}