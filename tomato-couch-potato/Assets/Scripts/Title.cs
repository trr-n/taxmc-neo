using trrne.Pancreas;
using UnityEngine;

namespace trrne.Eye
{
    public class Title : MonoBehaviour
    {
        void Update()
        {
            if (Inputs.Down(Constant.Keys.Button))
            {
                Scenes.Load(Constant.Scenes.Select);
            }
        }
    }
}
