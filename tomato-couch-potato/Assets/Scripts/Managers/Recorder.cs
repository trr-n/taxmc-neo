using trrne.Box;

namespace trrne.Brain
{
    public class Recorder : Singleton<Recorder>
    {
        protected override bool AliveOnLoad => true;

        public string Path => Paths.DataPath("save.sav");
        public string Password => "pomodoro";

        public int Max => 2;

        public int CurrentIndex => int.Parse(Scenes.Active().Delete(Constant.Scenes.Prefix));

        public int Stay { get; private set; }
        public int Done { get; private set; }

        public float Progress => Maths.Ratio(Max, Done);

        public void Clear()
        {
            // クリアしたシーンの名前に数字が含まれているか
            if (int.TryParse(Scenes.Active().Delete(Constant.Scenes.Prefix), out _))
            {
                ++Done;
                Scenes.Load(Constant.Scenes.Select);
            }
        }
    }
}