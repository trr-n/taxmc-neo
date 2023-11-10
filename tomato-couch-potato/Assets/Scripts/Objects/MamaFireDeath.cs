using Cysharp.Threading.Tasks;
using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class MamaFireDeath : MamaFire
    {
        [SerializeField]
        float chaseRange;

        bool isChase = true;
        Vector2 dir;

        protected override bool Tracking => true;

        protected override void Movement()
        {
            if (isChase && chaseRange <= Vector2.Distance(player.transform.position, transform.position))
            {
                isChase = false;
                dir = direction;
            }
            transform.Translate(Time.deltaTime * speed * dir.normalized);
        }

        protected override async UniTask Punishment(Player player) => await player.Die();

        protected override async void OnTriggerEnter2D(Collider2D info)
        {
            if (info.TryGetComponent(out Player player))
            {
                if (!player.IsDieProcessing)
                {
                    sr.SetAlpha(0);
                    effects.TryGenerate(transform.position);
                    await UniTask.WhenAll(Punishment(player));
                }
                Destroy(gameObject);
            }
        }
    }
}