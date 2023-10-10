using trrne.Bag;
using UnityEngine;

namespace trrne.Body
{
    public class ProgressRecorder : MonoBehaviour
    {
        public static int max => 2;
        static int currentint = 0;
        public static int current => int.Parse(Scenes.active.Split(Constant.Scenes.Prefix)[0]);
        public static string currentstr => Scenes.active;
        public static float progress => (float)currentint / max;

        public static void Next()
        {
            currentint++;
        }

        bool loaded = false;
        void Start()
        {
            Physics2D.gravity = Vector100.gravity;

            Load();
        }

        void Load()
        {
            if (loaded)
            {
                return;
            }
            print("load.");
            Scenes.LoadAdditive(Constant.Scenes.StageSelect);
            loaded = true;
        }
    }
}