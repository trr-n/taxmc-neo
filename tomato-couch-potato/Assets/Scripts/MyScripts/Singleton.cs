using System;
using UnityEngine;

namespace trrne.Box
{
    public abstract class Singleton<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        protected virtual bool AliveOnLoad { get; } = true;

        static T instance;
        public static T Instance => Shorthand.L1ne(() =>
        {
            if (!instance)
            {
                instance = (T)FindObjectOfType(typeof(T));
            }
            return instance;
        });

        protected virtual void Awake()
        {
            if (this != Instance)
            {
                Destroy(this);
            }

            if (AliveOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}

// https://nobushiueshi.com/unitymonobehaviour%E3%82%92%E3%82%B7%E3%83%B3%E3%82%B0%E3%83%AB%E3%83%88%E3%83%B3singleton%E3%81%AB%E3%81%99%E3%82%8B%E6%96%B9%E6%B3%95/