using Cysharp.Threading.Tasks;
using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class Spectre : Creature
    {
        [SerializeField]
        float lifetime = 30f;

        // GameObject playerObj;
        Player player;
        readonly Stopwatch lifetimeSW = new(true);
        readonly float fadeSpeed = 2;

        readonly float speed = 0.5f;
        float Left = 1;
        Vector2 Mirrored => new(-1, 1);

        protected override void Start()
        {
            Enable = true;
            base.Start();

            player = Gobject.GetWithTag<Player>(Constant.Tags.Player);
            Left = transform.localScale.x;
        }

        protected override async void Behavior()
        {
            Flip();

            // death
            if (this != null && lifetimeSW.Sf >= lifetime)
            {
                await Die();
            }
        }

        void Flip()
        {
            // flip the sprite
            if ((transform.position.x > player.transform.position.x && Left != transform.localScale.x)
                || (transform.position.x < player.transform.position.x && Left == transform.localScale.x))
            {
                transform.localScale *= Mirrored;
            }
        }

        /// <summary>
        /// ゆっくりプレイヤーを追随する
        /// </summary>
        protected override void Movement()
        {
            var direction = player.transform.position + player.CoreOffset - transform.position;
            transform.Translate(Time.deltaTime * speed * direction.normalized);
        }

        public override async UniTask Die() => await Die(UniTask.Delay(0));

        public async UniTask Die(UniTask task)
        {
            lifetimeSW.Reset();

            float alpha = 1;
            while (alpha >= 0 && this != null)
            {
                alpha -= Time.deltaTime * fadeSpeed;
                if (Maths.Twins(0, alpha))
                {
                    sr.SetAlpha(0);
                }
                sr.SetAlpha(alpha);
                await UniTask.Yield();
            }

            await UniTask.WhenAll(task);
            Destroy(gameObject);
        }

        async void OnTriggerEnter2D(Collider2D info)
        {
            if (info.TryGet(out Player player))
            {
                lifetimeSW.Reset();
                await Die(player.Die());
                await player.Die();
            }
        }
    }
}
