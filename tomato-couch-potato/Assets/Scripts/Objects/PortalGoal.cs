using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class PortalGoal : MonoBehaviour
    {
        [SerializeField]
        bool IsUpdate = false;
        public Vector2 Goal { get; private set; }

        void Start()
        {
            Goal = transform.position;
#if DEBUG
            GetComponent<SpriteRenderer>().SetAlpha(1);
#else
            GetComponent<SpriteRenderer>().SetAlpha(0);
#endif
        }

        void Update()
        {
            if (!IsUpdate)
            {
                return;
            }
            Goal = transform.position;
        }
    }
}