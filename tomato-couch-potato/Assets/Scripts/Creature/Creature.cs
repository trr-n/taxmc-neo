using Cysharp.Threading.Tasks;
using UnityEngine;

namespace trrne.Core
{
    public abstract class Creature : MonoBehaviour, ICreature
    {
        [SerializeField]
        protected GameObject diefx;

        public bool Enable { get; set; }

        protected SpriteRenderer sr;

        protected virtual void Start()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        protected virtual void Update()
        {
            if (this == null || !Enable)
            {
                return;
            }

            Movement();
            Behavior();
        }

        protected abstract void Movement();
        protected abstract void Behavior();
        public abstract UniTask Die();
    }
}
