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

        protected abstract bool UpdateDirection { get; }

        public bool Enable { get; set; } = true;

        protected SpriteRenderer sr;

        // protected Mama mama;
        // public void SetMama(Mama mama) => this.mama = mama;

        protected Vector2 direction;

        GameObject player;

        protected virtual void Start()
        {
            sr = GetComponent<SpriteRenderer>();

            player = Gobject.Find(Constant.Tags.Player);
            direction = player.transform.position - transform.position;
        }

        protected virtual void Update()
        {
            if (Enable)
            {
                Movement();
                if (UpdateDirection)
                {
                    direction = player.transform.position - transform.position;
                }
            }
        }

        protected abstract void Movement();

        protected abstract UniTask Punishment(Player player);

        protected abstract void OnTriggerEnter2D(Collider2D other);
    }
}