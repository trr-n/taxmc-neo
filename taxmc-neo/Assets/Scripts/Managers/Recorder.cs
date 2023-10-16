using System;
using trrne.WisdomTeeth;
using UnityEngine;

namespace trrne.Brain
{
    public class Recorder : Singleton<Recorder>
    {
        protected override bool alive => true;

        (int stay, int cleared) idx;

        /// <summary>
        /// ステージ数
        /// </summary>
        public int max => 2;

        public int current => int.Parse(Scenes.active.Delete(Constant.Scenes.Prefix));

        public int stay => idx.stay;

        /// <summary>
        /// 進捗
        /// </summary>
        public float progress => (float)idx.cleared / max;

        /// <summary>
        /// 次のステージに
        /// </summary>
        public void Next(string name)
        {
            try
            {
                if (int.TryParse(name.Delete(Constant.Scenes.Prefix), out int idx))
                {
                    this.idx.stay = idx;
                    Scenes.Load(name);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        ///! シーン遷移前に実行すること
        /// </summary>
        public void Clear()
        {
            // クリアしたと思われるシーンにPrefixと整数が含まれていたら+1
            if (int.TryParse(Scenes.active.Delete(Constant.Scenes.Prefix), out _))
            {
                idx.cleared++;
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
            print($"inProgress: {stay}\ncleared: {idx.cleared}\ncurrent: {current}");
        }
    }
}