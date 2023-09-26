using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.SceneManagement.SceneManager;

namespace trrne.Bag
{
    public class Scenes
    {
        /// <summary>
        /// nameで指定したシーンをロード
        /// </summary>
        /// <param name="name">name</param>
        public static void Load(string name) => LoadScene(name);

        /// <summary>
        /// build indexで指定したシーンをロード
        /// </summary>
        /// <param name="index">build index</param>
        public static void Load(int index) => LoadScene(index);

        /// <summary>
        /// シーンをリロード
        /// </summary>
        public static void Load() => LoadScene(active);

        /// <summary>
        /// アクティブなシーンを取得
        /// </summary>
        public static string active => GetActiveScene().name;

        /// <summary>
        /// 全てのシーンの名前
        /// </summary>
        public static string[] names
        {
            get
            {
                var names = new string[sceneCount];
                for (int i = 0; i < names.Length; i++)
                {
                    names[i] = GetSceneByBuildIndex(i).name;
                }

                return names.Length switch
                {
                    0 => throw new Karappoyanke("tabun settei sitenai"),
                    _ => names
                };
            }
        }

        /// <summary>
        /// シーン数
        /// </summary>
        public static int total => names.Length;

        public static AsyncOperation LoadAsync(string name) => LoadSceneAsync(name);
        public static AsyncOperation LoadAsync(int index) => LoadSceneAsync(index);
    }
}
