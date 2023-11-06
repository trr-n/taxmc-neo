using Cysharp.Threading.Tasks;
using UnityEngine;

namespace trrne.Core
{
    public abstract class MamaFireBase : MonoBehaviour
    {
        [SerializeField]
        protected GameObject[] effects;

        protected float speed;
        protected float effectDuration;

        protected SpriteRenderer sr;

        protected Mama mama;
        public void SetMama(Mama mama) => this.mama = mama;

        protected virtual void Start()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        protected abstract void Movement();
        protected abstract UniTask Punishment(Player player);
        protected abstract void OnTriggerEnter2D(Collider2D other);
    }
}