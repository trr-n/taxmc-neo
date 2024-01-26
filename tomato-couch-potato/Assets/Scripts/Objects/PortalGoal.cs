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