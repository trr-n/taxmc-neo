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
            if (!isClose && Vec.Distance(transform, player.Core) <= chaseRange)
            {
                isClose = true;
                isTracking = false;
            }
            transform.Translate(Time.deltaTime * speed * direction);
        }

        protected override async UniTask Punishment(Player player) => await player.Die();
    }
}