using trrne.Box;
using UnityEngine;

namespace trrne.Eye
{
    public class Title : MonoBehaviour
    {
        void Update()
        {
            if (Inputs.Down(Config.Keys.Button))
            {
                Scenes.Load(Config.Scenes.Select);
            }
        }
    }
}
