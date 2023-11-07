using trrne.Box;

namespace trrne.Brain
{
    public class Recorder : Singleton<Recorder>
    {
        protected override bool LiveOnLoad => true;

        public string Path => Paths.Data + "/save.sav";
        public string Password => "pomodoro";

        // (int stay, int done) idx = (0, 0);

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
        public int Stay { get; private set; }

        /// <summary>
        /// クリア済み
        /// </summary>
        public int Done { get; private set; }

        /// <summary>
        /// 進捗
        /// </summary>
        public float Progress => Maths.Ratio(Max, Done);

        public void Clear()
        {
            if (int.TryParse(Scenes.Active().Delete(Constant.Scenes.Prefix), out _))
            {
                Done++;
                Scenes.Load(Constant.Scenes.Select);
            }
        }
    }
}