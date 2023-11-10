using Cysharp.Threading.Tasks;
using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class MamaFireMirror : MamaFire
    {
        protected override bool Tracking => false;

        protected override void Movement() => transform.Translate(Time.deltaTime * speed * direction.normalized);
        protected override async UniTask Punishment(Player player) => await player.Punishment(effectDuration, EffectType.Mirror);

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
