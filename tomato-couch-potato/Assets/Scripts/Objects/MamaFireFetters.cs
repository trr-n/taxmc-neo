using Cysharp.Threading.Tasks;
using UnityEngine;
using trrne.Box;

namespace trrne.Core
{
    public class MamaFireFetters : MamaFireBase
    {
        [SerializeField]
        float detectRange = 10;

        protected override void Start()
        {
            base.Start();
            isTracking = false;
        }

        protected override void Movement()
        {
            // プレイヤーとの距離がdetectRange以下なら追随する
            isTracking = Vec.Distance(transform, player.Core) < detectRange;
            transform.Translate(Time.deltaTime * speed * direction);
        }

        protected override async UniTask Punishment(Player player) => await player.Punishment(effectDuration, PunishEffect.Fetters);
    }
}