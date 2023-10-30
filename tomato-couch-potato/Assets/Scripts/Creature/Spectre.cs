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
        Stopwatch lifetimeSW = new(true);
        readonly float fadeSpeed = 10;

        readonly float speed = 0.5f;

        protected override void Start()
        {
            Enable = true;
            base.Start();

            // playerObj = Gobject.Find(Constant.Tags.Player);
            player = Gobject.GetWithTag<Player>(Constant.Tags.Player);
        }

        protected override async void Behavior()
        {
            if (lifetimeSW.Sf >= lifetime)
            {
                await Die();
            }
        }

        protected override void Movement()
        {
            var direction = player.transform.position - transform.position + player.Offset.ToVec3();
            transform.Translate(direction.normalized * speed * Time.deltaTime);
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
