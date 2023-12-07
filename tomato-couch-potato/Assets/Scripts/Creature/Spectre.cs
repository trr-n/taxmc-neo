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
        // const float FadeSpeed = 2;
        // const float MoveSpeed = 0.5f;
        readonly (float fade, float move) speed = (2f, 0.5f);
        float left = 0f;
        Vector2 Mirrored => new(-1, 1);

        protected override void Start()
        {
            Enable = true;
            base.Start();

            player = Gobject.GetWithTag<Player>(Config.Tags.Player);
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
            float selfx = transform.position.x,
                playerx = player.transform.position.x,
                scalex = transform.localScale.x;
            if ((selfx > playerx && left != scalex)
                || (selfx < playerx && left == scalex))
            {
                transform.localScale *= Mirrored;
            }
        }

        // follow the player slowly
        protected override void Movement()
        {
            var direction = player.Core - transform.position;
            transform.Translate(Time.deltaTime * speed.move * direction.normalized);
        }

        public override async UniTask Die() => await Die(UniTask.Delay(0));

        public async UniTask Die(UniTask _task)
        {
            lifetimeSW.Reset();
            float alpha = 1f;
            while (alpha >= 0 && this != null)
            {
                alpha -= Time.deltaTime * speed.fade;
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
