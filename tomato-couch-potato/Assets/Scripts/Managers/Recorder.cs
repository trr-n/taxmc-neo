using System.IO;
using trrne.Pancreas;

namespace trrne.Brain
{
    public class Recorder : Singleton<Recorder>
    {
        protected override bool Alive => true;

        public (string Path, string Password) Secret => (Paths.Data + "/save.sav", "pomodoro");

        (int stay, int done) idx = (0, 0);

        /// <summary>
        /// ステージ数
        /// </summary>
        public int Max => 2;

        /// <summary>
        /// プレイ中のステージ
        /// </summary>
        public int Current => int.Parse(Scenes.Active().Delete(Constant.Scenes.Prefix));

        public int Stay => idx.stay;
        public int Done => idx.done;

        /// <summary>
        /// 進捗
        /// </summary>
        public float Progress => (float)idx.done / Max;

        public void Clear()
        {
            // クリアしたと思われるシーンにPrefixと整数が含まれていたら+1
            if (int.TryParse(Scenes.Active().Delete(Constant.Scenes.Prefix), out int idx))
            {
                this.idx.done++;
                // Scenes.Load(Constant.Scenes.Prefix + idx);
                Scenes.Load(Constant.Scenes.Select);
            }
        }

        public void Save()
        {
            Pancreas.Save.Write(idx.done, Secret.Password, Secret.Path, FileMode.Append);
        }
    }
}