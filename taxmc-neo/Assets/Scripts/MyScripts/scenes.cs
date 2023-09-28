using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
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

        public static int total => sceneCountInBuildSettings;

        public static AsyncOperation LoadAsync(string name) => LoadSceneAsync(name);
        public static AsyncOperation LoadAsync(int index) => LoadSceneAsync(index);
    }
}
