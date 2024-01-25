#if !UNITY_EDITOR
using trrne.Box;
#endif
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
#if !UNITY_EDITOR
            GetComponent<SpriteRenderer>().SetAlpha(0);
#endif
        }

        void Update()
        {
            if (IsUpdate)
            {
                Goal = transform.position;
            }
        }
    }
}