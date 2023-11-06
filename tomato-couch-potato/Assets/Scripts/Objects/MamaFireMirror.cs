using System.Collections.ObjectModel;
using Cysharp.Threading.Tasks;
using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class MamaFireMirror : MamaFireBase
    {
        Vector2 direction;
        readonly Stopwatch sw = new();

        protected override void Start()
        {
            base.Start();
            direction = Gobject.Find(Constant.Tags.Player).transform.position - transform.position;
        }

        protected override void Movement()
        {
            transform.Translate(Time.deltaTime * speed * direction.normalized);
        }

        protected override async UniTask Punishment(Player player)
        {
            await UniTask.Yield();

            if (player.Mirrorable)
            {
                sw.Start();
                player.IsMirroring = true;
            }

            if (sw.Sf >= effectDuration)
            {
                player.IsMirroring = false;
            }
        }

        protected override async void OnTriggerEnter2D(Collider2D info)
        {
            if (info.TryGet(out Player player) && !player.IsDieProcessing)
            {
                sr.SetAlpha(0);
                effects.TryGenerate(transform.position);

                await Punishment(player);
                await UniTask.WhenAll(Punishment(player));
                await player.Die();

                // プレイヤーの死亡処理が終わったら自分を破壊
                await UniTask.WhenAll(player.Die());
                Destroy(gameObject);
            }
        }
    }
}
