using Cysharp.Threading.Tasks;
using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class MamaFireMirror : MamaFire
    {
        protected override void Start()
        {
            base.Start();
            isTracking = false;
        }

        protected override void Movement() => transform.Translate(Time.deltaTime * speed * direction);
        protected override async UniTask Punishment(Player player) => await player.Punishment(effectDuration, EffectType.Mirror);

        protected override async void OnTriggerEnter2D(Collider2D info)
        {
            if (info.TryGetComponent(out Player player))
            {
                effects.TryInstantiate(transform.position);
                sr.SetAlpha(0);
                if (!player.IsDieProcessing)
                {
                    await UniTask.WhenAll(Punishment(player));
                }
                try { Destroy(gameObject); }
                catch { }
            }
        }
    }
}
