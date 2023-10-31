using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using trrne.Pancreas;
using UnityEngine;

namespace trrne.Heart
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

        async UniTask Die(bool boo)
        {
            sr.SetAlpha(0);
            effects.TryGenerate(transform.position);
            await UniTask.WaitUntil(() => boo);
            std.cout("finish!" + std.endl());
            Destroy(gameObject);
        }

        async void OnTriggerEnter2D(Collider2D info)
        {
            if (info.TryGet(out Player player))
            {
                await player.Die();
                await Die(!player.IsDieProcessing);
            }
        }
    }
}
