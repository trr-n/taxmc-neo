using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class LeverFlag : MonoBehaviour
    {
        bool hit;
        public bool Hit => hit;

        void OnTriggerEnter2D(Collider2D info)
        {
            if (info.TryGet(out Player _))
            {
                hit = true;
            }
        }

        void OnTriggerExit2D(Collider2D info)
        {
            if (info.TryGet(out Player _))
            {
                hit = false;
            }
        }
    }
}
