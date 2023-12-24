using Cysharp.Threading.Tasks;
using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class Spectre : Creature
    {
        [SerializeField]
        float lifetime = 30f;

        Player player;
        readonly Stopwatch lifetimeSW = new(true);
        const float FADE_SPEED = 2f;
        const float MOVE_SPEED = .5f;
        float left = 0f;
        Vector2 mirrored => new(-1, 1);

        protected override void Start()
        {
            Enable = true;
            base.Start();

            player = Gobject.GetWithTag<Player>(Config.Tags.PLAYER);
            left = transform.localScale.x;
        }

        protected override async void Behavior()
        {
            Flip();

            if (this != null && lifetimeSW.Sf() >= lifetime)
            {
                await Die();
            }
        }

        // flip the sprite
        void Flip()
        {
            float self = transform.position.x,
                player = this.player.transform.position.x,
                scale = transform.localScale.x;
            if ((self > player && left != scale)
                || (self < player && left == scale))
            {
                transform.localScale *= mirrored;
            }
        }

        // follow the player slowly
        protected override void Movement()
        {
            var direction = player.Core - transform.position;
            transform.Translate(Time.deltaTime * MOVE_SPEED * direction.normalized);
        }

        public override async UniTask Die() => await Die(UniTask.Delay(0));

        public async UniTask Die(UniTask _task)
        {
            lifetimeSW.Reset();
            float alpha = 1f;
            while (alpha >= 0 && this != null)
            {
                alpha -= Time.deltaTime * FADE_SPEED;
                sr.SetAlpha(alpha.Twins(0f) ? 0 : alpha);
                await UniTask.WaitForSeconds(Time.deltaTime);
            }
            await UniTask.WhenAll(_task);
            Destroy(gameObject);
        }

        async void OnTriggerEnter2D(Collider2D info)
        {
            if (info.TryGetComponent(out Player player))
            {
                lifetimeSW.Reset();
                await Die(player.Die());
                await player.Die();
            }
        }
    }
}
