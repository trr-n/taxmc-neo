using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class MamaFlag : MonoBehaviour
    {
        public bool IsInsideRange { get; private set; }

        void OnTriggerEnter2D(Collider2D info)
        {
            if (info.TryGetComponent(out Player _))
            {
                IsInsideRange = true;
            }
        }

        void OnTriggerExit2D(Collider2D info)
        {
            if (info.TryGetComponent(out Player _))
            {
                IsInsideRange = false;
            }
        }
    }
}
