using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class LeverFlag : MonoBehaviour
    {
        public bool Hit { get; private set; }

        void OnTriggerEnter2D(Collider2D info)
        {
            if (info.TryGetComponent(out Player _))
            {
                Hit = true;
            }
        }

        void OnTriggerExit2D(Collider2D info)
        {
            if (info.TryGetComponent(out Player _))
            {
                Hit = false;
            }
        }
    }
}
