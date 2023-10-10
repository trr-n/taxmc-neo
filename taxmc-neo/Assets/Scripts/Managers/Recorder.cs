using trrne.Bag;
using UnityEngine;

namespace trrne.Body
{
    public class Recorder : Singleton<Recorder>
    {
        protected override bool notDestroy => true;

        int playing = 0;
        int cleared = 0;

        /// <summary>
        /// ステージ数
        /// </summary>
        public int max => 2;

        public int current => int.Parse(Scenes.active.Split(Constant.Scenes.Prefix)[1]);

        public int playingIdx => playing;

        /// <summary>
        /// 進捗
        /// </summary>
        public float progress => (float)cleared / max;

        /// <summary>
        /// 次のステージに
        /// </summary>
        public void Next(string name)
        {
            playing++;
            Scenes.Load(name);
        }

        /// <summary>
        ///! シーン遷移前に実行すること
        /// </summary>
        public void Clear()
        {
            // クリアしたと思われるシーンにPrefixと整数が含まれていたら+1
            if (int.TryParse(Scenes.active.Split(Constant.Scenes.Prefix)[1], out _))
            {
                cleared++;
            }
        }

        void Start()
        {
            if (Scenes.active == Constant.Scenes.StageSelect)
            {
                Physics2D.gravity = Vector100.zero2d;
            }
        }

        void Update()
        {
            print($"inProgress: {playingIdx}\ncleared: {cleared}\ncurrent: {current}");
        }
    }
}