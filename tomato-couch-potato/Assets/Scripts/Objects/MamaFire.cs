using Cysharp.Threading.Tasks;
using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class MamaFire : MonoBehaviour
    {
        [SerializeField]
        protected GameObject[] effects;

        [SerializeField]
        protected float speed = 5;

        [SerializeField]
        protected float effectDuration = 3;

        [SerializeField]
        protected float life = 30f;

        protected bool isTracking = true;

        public bool Enable { get; set; }

        protected CircleCollider2D hitbox;
        protected SpriteRenderer sr;
        protected Vector2 direction;

        protected Player player { get; private set; }

        protected virtual void Start()
        {
            Enable = true;

            sr = GetComponent<SpriteRenderer>();
            hitbox = GetComponent<CircleCollider2D>();
            hitbox.isTrigger = true;

            // playerObj = Gobject.Find(Constant.Tags.Player);
            // player = playerObj.GetComponent<Player>();
            player = Gobject.GetComponentWithTag<Player>(Constant.Tags.Player);
            direction = (player.CoreOffset - transform.position).normalized;

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
            if (isTracking)
            {
                direction = (player.transform.position - transform.position).normalized;
            }
        }

        protected abstract void Movement();
        protected abstract UniTask Punishment(Player player);

        protected virtual async void OnTriggerEnter2D(Collider2D info)
        {
            if (info.TryGetComponent(out Player player))
            {
                sr.SetAlpha(0);
                effects.TryInstantiate(transform.position);
                if (!player.IsDying)
                {
                    await UniTask.WhenAll(Punishment(player));
                }
                try { Destroy(gameObject); }
                catch { }
            }
        }
    }
}