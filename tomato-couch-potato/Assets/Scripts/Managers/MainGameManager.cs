using trrne.Box;
using trrne.Secret;

namespace trrne.Brain
{
    public class MainGameManager : Singleton<MainGameManager>
    {
        protected override bool DontDestroy => true;

        public const int MAX = 2;
        public int Current => int.Parse(Scenes.Active().Delete(Constant.Scenes.PREFIX));
        public int Stay { get; private set; }
        public int Done { get; private set; }

        public float Progress => MF.Ratio(MAX, Done);

        public string[] Scores { get; private set; } = new string[MAX];

        public void SceneTransition(string name) => Scenes.Load(name);

        public void ClearStage()
        {
            // クリアしたシーンの名前に数字が含まれているか
            if (int.TryParse(Scenes.Active().Delete(Constant.Scenes.PREFIX), out int index))
            {
                ++Done;
                // Transition(Config.Scenes.SELECT);
            }
        }

        public string Path(string name) => App.DataPath(name + ".sav");
        public const string PASSWORD = "pomodoro";
        public void WriteSaveData(string fileName, string src) => Save.Write(src, PASSWORD, Path(fileName));
        public string ReadSaveData(string fileName) => Save.Read<string>(PASSWORD, Path(fileName));
    }
}