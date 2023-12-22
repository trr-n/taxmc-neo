using Cysharp.Threading.Tasks;
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
            var distance = Vector2.Distance(transform.position, player.Core);
            if (!isClose && distance <= chaseRange)
            {
                isClose = true;
                isTracking = false;
            }
            transform.Translate(Time.deltaTime * speed * direction);
        }

        protected override async UniTask Punishment(Player player) => await player.Die();
    }
}