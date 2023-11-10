using Cysharp.Threading.Tasks;
using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public abstract class MamaFire : MonoBehaviour
    {
        [SerializeField]
        protected GameObject[] effects;

        [SerializeField]
        protected float speed = 5;

        [SerializeField]
        protected float effectDuration = 3;

        protected abstract bool Tracking { get; }

        public bool Enable { get; set; }

        protected BoxCollider2D hitbox;
        protected SpriteRenderer sr;
        protected Vector2 direction;
        protected float life = 30f;

        protected GameObject player { get; private set; }

        protected virtual void Start()
        {
            Enable = true;

            sr = GetComponent<SpriteRenderer>();
            hitbox = GetComponent<BoxCollider2D>();
            hitbox.isTrigger = true;

            player = Gobject.Find(Constant.Tags.Player);

            direction = player.transform.position - transform.position;

            // life秒後に破壊
            Destroy(gameObject, life);
        }

        protected virtual void Update()
        {
            if (!Enable)
            {
                return;
            }

            Movement();
            if (Tracking)
            {
                direction = player.transform.position - transform.position;
            }
        }

        protected abstract void Movement();
        protected abstract void OnTriggerEnter2D(Collider2D info);
        protected abstract UniTask Punishment(Player player);
    }
}