using Cysharp.Threading.Tasks;
using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class MamaFireFetters : MamaFire
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
            float distance = Vector2.Distance(transform.position, player.CoreOffset);
            isTracking = distance < detectRange;
            transform.Translate(Time.deltaTime * speed * direction);
        }

        protected override async UniTask Punishment(Player player) => await player.Punishment(effectDuration, EffectType.Fetters);
    }
}