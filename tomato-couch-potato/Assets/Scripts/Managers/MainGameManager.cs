using System;
using UnityEngine;
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
            if (int.TryParse(Scenes.Active().Delete(Constant.Scenes.PREFIX), out _))
            {
                ++Done;
            }
        }

        public string Path(string name) => App.DataPath(name + ".sav");
        public const string PASSWORD = "pomodoro";
        public void WriteSaveData(string fileName, string src) => Save.Write(src, PASSWORD, Path(fileName));
        public string ReadSaveData(string fileName) => Save.Read<string>(PASSWORD, Path(fileName));

        AudioSource speaker;
        float volume = 0.2f;

        void Start()
        {
            print("start func of singleton");
            speaker = GetComponent<AudioSource>();
            InitVolumeSettings();
            speaker.Play();
        }

        void Update()
        {
            // if (Inputs.Down(Constant.Keys.CHANGE_MUSIC_VOLUME))
            // {
            //     volume = 0;
            // }
            // print("call");
            // var axis = Input.GetAxis(Constant.Keys.CHANGE_MUSIC_VOLUME);
            // if (MF.ZeroTwins(axis))
            // {
            //     return;
            // }
            // volume += axis / 8;
            // speaker.volume = Mathf.Clamp01(volume);
            // print(volume);

            var axis = Input.GetAxisRaw(Constant.Keys.CHANGE_MUSIC_VOLUME);
            if (MF.ZeroTwins(axis))
            {
                return;
            }

            volume += MathF.Sign(axis) * 0.1f;
            FetchValue(volume = Mathf.Clamp01(volume));
        }

        void FetchValue(float value) => speaker.volume = value;

        public void InitVolumeSettings()
        {
            if (speaker != null)
            {
                speaker.loop = true;
                speaker.volume = volume;
            }
        }
    }
}