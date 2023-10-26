using trrne.Pancreas;

namespace trrne.Brain
{
    public class Recorder : Singleton<Recorder>
    {
        protected override bool Alive => true;

        public string Path => Paths.Data + "/save.sav";
        public string Password => "pomodoro";

        (int stay, int done) idx = (0, 0);

        /// <summary>
        /// ステージ数
        /// </summary>
        public int Max => 2;

        /// <summary>
        /// プレイ中のステージ
        /// </summary>
        public int Current => int.Parse(Scenes.Active().Delete(Constant.Scenes.Prefix));

        /// <summary>
        /// プレイ中のステージ
        /// </summary>
        public int Stay => idx.stay;

        /// <summary>
        /// クリア済み
        /// </summary>
        public int Done => idx.done;

        /// <summary>
        /// 進捗
        /// </summary>
        public float Progress => Maths.Ratio(Max, idx.done);

        public void Clear()
        {
            if (int.TryParse(Scenes.Active().Delete(Constant.Scenes.Prefix), out _))
            {
                idx.done++;
                Scenes.Load(Constant.Scenes.Select);
            }
        }
    }
}