using Cysharp.Threading.Tasks;
using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class MamaFireDeath : MamaFire
    {
        [SerializeField]
        float chaseRange;

        bool isClose = false;

        protected override void Movement()
        {
            // プレイヤーとの距離がchaseRange以下になるまで追随、以下になったらそのまま直進
            var distance = Vector2.Distance(transform.position, player.CoreOffset);
            if (!isClose && distance <= chaseRange)
            {
                isClose = true;
                isTracking = false;
            }
            transform.Translate(Time.deltaTime * speed * direction);
        }

        protected override async UniTask Punishment(Player player) => await player.Die();

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

                try
                {
                    Destroy(gameObject);
                }
                catch (MissingReferenceException e) { print(e.Message); }
            }
        }
    }
}