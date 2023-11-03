using Cysharp.Threading.Tasks;
using trrne.Pancreas;
using UnityEngine;

namespace trrne.Heart
{
    public class Spectre : Creature
    {
        [SerializeField]
        float lifetime = 30f;

        // GameObject playerObj;
        Player player;
        readonly Stopwatch lifetimeSW = new(true);
        readonly float fadeSpeed = 10;

        readonly float speed = 0.5f;

        protected override void Start()
        {
            Enable = true;
            base.Start();

            // playerObj = Gobject.Find(   Constant.Tags.Player);
            player = Gobject.GetWithTag<Player>(Constant.Tags.Player);
        }

        protected override async void Behavior()
        {
            if (this != null && lifetimeSW.Sf >= lifetime * 100) //! DEBUG //////////////////////////////////////////////////////////////////
            {
                await Die();
            }
        }

        protected override void Movement()
        {
            var direction = player.transform.position + player.CoreOffset - transform.position;
            // var rotate = Quaternion.FromToRotation(Vector2.up, direction);

            // transform.SetRotation(z: rotate.z, w: rotate.w);
            transform.Translate(Time.deltaTime * speed * direction); // Vector2.up);
        }

        public override async UniTask Die()
        {
            lifetimeSW.Reset();

            float alpha = 1;
            while (alpha >= 0)
            {
                alpha -= Time.deltaTime * fadeSpeed;

                if (Maths.Twins(0, alpha))
                {
                    sr.SetAlpha(0);
                }

                sr.SetAlpha(alpha);
            }
            await UniTask.NextFrame();
            Destroy(gameObject);
        }

        async void OnTriggerEnter2D(Collider2D info)
        {
            if (info.TryGet(out ICreature creature))
            {
                lifetimeSW.Reset();
                await Die();

                await creature.Die();
            }
        }
    }
}
