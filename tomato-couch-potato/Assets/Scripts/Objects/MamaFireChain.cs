using Cysharp.Threading.Tasks;
using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class MamaFireChain : MamaFire
    {
        protected override void Start()
        {
            base.Start();
            isTracking = false;
        }

        protected override void Movement() => transform.Translate(Time.deltaTime * speed * direction);
        protected override async UniTask Punishment(Player player) => await player.Punishment(effectDuration, EffectType.Chain);

        /* protected override async void OnTriggerEnter2D(Collider2D info)
        {
            if (info.TryGetComponent(out Player player))
            {
                sr.SetAlpha(0);
                effects.TryInstantiate(transform.position);
                if (!player.IsDieProcessing)
                {
                    await UniTask.WhenAll(Punishment(player));
                }
                Destroy(gameObject);
            }
        } */
    }
}
