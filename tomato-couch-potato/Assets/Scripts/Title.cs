using trrne.Box;
using UnityEngine;

namespace trrne.Eye
{
    public class Title : MonoBehaviour
    {
        void Update()
        {
            if (Inputs.Down(Constant.Keys.BUTTON))
            {
                Scenes.Load(Constant.Scenes.SELECT);
            }
        }
    }
}