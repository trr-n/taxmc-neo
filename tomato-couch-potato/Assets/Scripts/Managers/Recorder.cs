using trrne.Box;
using UnityEngine;

namespace trrne.Brain
{
    public class Recorder : Singleton<Recorder>
    {
        protected override bool dontDestroy => true;

        public string Path => Paths.DataPath("save.sav");
        public string Password => "pomodoro";

        public int Max => 2;

        public int CurrentIndex => int.Parse(Scenes.Active().Delete(Config.Scenes.PREFIX));

        public int Stay { get; private set; }
        public int Done { get; private set; }

        public float Progress => Numcs.Ratio(Max, Done);

        AudioSource source;

        protected override void Awake()
        {
            base.Awake();
            source = GetComponent<AudioSource>();
        }

        public void Clear()
        {
            // クリアしたシーンの名前に数字が含まれているか
            if (int.TryParse(Scenes.Active().Delete(Config.Scenes.PREFIX), out _))
            {
                ++Done;
                Scenes.Load(Config.Scenes.SELECT);
            }
        }

        public void PlayOneShot(AudioClip clip) => source.PlayOneShot(clip);
    }
}