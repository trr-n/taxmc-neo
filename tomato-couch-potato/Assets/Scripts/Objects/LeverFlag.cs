using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class LeverFlag : MonoBehaviour
    {
        public bool Hit { get; private set; }

        void OnTriggerEnter2D(Collider2D info)
        {
            if (Gobject.TryGetComponent(info, out Player _))
            {
                Hit = true;
            }
        }

        void OnTriggerExit2D(Collider2D info)
        {
            if (Gobject.TryGetComponent(info, out Player _))
            {
                Hit = false;
            }
        }
    }
}
