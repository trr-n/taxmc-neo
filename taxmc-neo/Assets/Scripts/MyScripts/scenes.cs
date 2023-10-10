using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.SceneManagement.SceneManager;

namespace trrne.Bag
{
    public enum Counting { Built, Unbuilt }

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

        public static void LoadAdditive(string name) => LoadScene(name, LoadSceneMode.Additive);

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
        }

        public static int Total(Counting which) => which switch
        {
            Counting.Unbuilt => sceneCountInBuildSettings,
            Counting.Built => sceneCount,
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
