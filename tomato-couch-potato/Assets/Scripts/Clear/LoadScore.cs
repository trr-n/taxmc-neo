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

        void Update()
        {
            scoreT.SetText(string.Join('\n', MainGameManager.Instance.Scores));

            if (Inputs.Down(Constant.Keys.BUTTON))
            {
                SceneManager.Title();
            }
        }
    }
}