using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class PortalGoal : MonoBehaviour
    {
        [SerializeField]
        bool IsUpdate = false;
        public Vector2 Goal { get; private set; }

        SpriteRenderer sr;

        void Start()
        {
            Goal = transform.position;
            sr = GetComponent<SpriteRenderer>();
#if DEBUG
            sr.SetAlpha(1);
#else
            sr.SetAlpha(0);
#endif
        }

        void Update()
        {
            if (!IsUpdate)
                return;
            Goal = transform.position;
        }
    }
}