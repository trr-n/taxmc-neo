using trrne.Box;
using trrne.Brain;
using UnityEngine;
using UnityEngine.UI;

namespace trrne.Core
{
    public class LoadScore : MonoBehaviour
    {
        [SerializeField]
        Text scoreT;

        [SerializeField]
        string[] stageNames = { "チュートリアル", "メイン" };

        string scoreStr;

        void Start()
        {
            var scores = MainGameManager.Instance.Scores;
            for (int i = 0; i < MainGameManager.MAX; ++i)
            {
                scoreStr += $"{stageNames[i]}: {scores[i]}";
                if (i < MainGameManager.MAX - 1)
                {
                    scoreStr += '\n';
                }
            }
        }

        void Update()
        {
            scoreT.SetText(scoreStr);

            if (Inputs.Down(Constant.Keys.BUTTON))
            {
                SceneManager.Title();
            }
        }
    }
}