using Chickenen.Pancreas;
using UnityEngine;

namespace Chickenen.Eye
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
