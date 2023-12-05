using UnityEngine;

namespace trrne.Core
{
    public class MamaFlag : MonoBehaviour
    {
        public bool OnRange { get; private set; }

        void OnTriggerEnter2D(Collider2D info)
        {
            if (info.TryGetComponent(out Player _))
            {
                OnRange = true;
            }
        }

        void OnTriggerExit2D(Collider2D info)
        {
            if (info.TryGetComponent(out Player _))
            {
                OnRange = false;
            }
        }
    }
}
