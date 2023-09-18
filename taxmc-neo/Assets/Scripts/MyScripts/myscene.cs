using UnityEngine;
using static UnityEngine.SceneManagement.SceneManager;

namespace trrne.Utils
{
    public class MyScene
    {
        public static void Load(string name) => LoadScene(name);
        public static void Load(int index) => LoadScene(index);
        public static void Load() => LoadScene(Active());

        public static string Active() => GetActiveScene().name;

        public static AsyncOperation LoadAsync(string name) => LoadSceneAsync(name);
        public static AsyncOperation LoadAsync(int index) => LoadSceneAsync(index);
    }
}
