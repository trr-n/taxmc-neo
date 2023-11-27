using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.SceneManagement.SceneManager;

namespace trrne.Box
{
    public static class Scenes
    {
        public static void Load(string name) => LoadScene(name);
        public static void Load(int index) => LoadScene(index);
        public static void Load() => LoadScene(Active());
        public static void LoadAdditive(string name) => LoadScene(name, LoadSceneMode.Additive);

        public static string Active() => GetActiveScene().name;

        public static string[] Names()
        {
            string[] names = new string[sceneCount];
            for (int i = 0; i < sceneCount; i++)
            {
                names[i] = GetSceneAt(i).name;
            }
            return names.Length switch
            {
                0 => throw new Karappoyanke("tabun settei sitenai"),
                _ => names
            };
        }

        public static int Total(ScenesCountingFormat which) => which switch
        {
            ScenesCountingFormat.Unbuilt => sceneCountInBuildSettings,
            ScenesCountingFormat.Built => sceneCount,
            _ => -1
        };

        public static AsyncOperation LoadAsync(string name) => LoadSceneAsync(name);
        public static AsyncOperation LoadAsync(string name, LoadSceneMode mode) => LoadSceneAsync(name, mode);
        public static AsyncOperation LoadAsync(int index) => LoadSceneAsync(index);
        public static AsyncOperation LoadAsync(int index, LoadSceneMode mode) => LoadSceneAsync(index, mode);

        public static AsyncOperation UnloadAsync(string name) => UnloadSceneAsync(name);
        public static AsyncOperation UnloadAsync(string name, UnloadSceneOptions options) => UnloadSceneAsync(name, options);
        public static AsyncOperation UnloadAsync(int index) => UnloadSceneAsync(index);
        public static AsyncOperation UnloadAsync(int index, UnloadSceneOptions options) => UnloadSceneAsync(index, options);
    }
}
