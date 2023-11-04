using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class MamaFire : Object
    {
        readonly float speed = 5;
        GameObject player;

        protected override void Start()
        {
            base.Start();
            player = Gobject.Find(Constant.Tags.Player);
        }

        protected override void Behavior()
        {
            var direction = player.transform.position - transform.position;
            transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
            transform.Translate(Time.deltaTime * speed * Vector2.up);
        }

        async void OnTriggerEnter2D(Collider2D info)
        {
            if (info.TryGet(out Player player) && !player.IsDieProcessing)
            {
                await player.Die();

                sr.SetAlpha(0);
                effects.TryGenerate(transform.position);

                // プレイヤーの死亡処理が終わったら自分を破壊
                await UniTask.WhenAll(player.Die());
                Destroy(gameObject);
            }
        }
    }
}
