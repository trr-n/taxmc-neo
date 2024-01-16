using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class Barrel : Object
    {
        [SerializeField]
        float power = 0f;

        [SerializeField]
        [Tooltip("回転角")]
        float range;

        bool onBarrel = false;
        float init;

        protected override void Start()
        {
            base.Start();
            init = transform.eulerAngles.z;
        }

        protected override void Behavior()
        {
            if (!onBarrel)
            {
                return;
            }
            // 樽を回転させてキーが押された時点の角度に合わせて飛ばす
            float z = 0;
            // TODO 回転処理
            z = Mathf.Clamp(z, init - range, init + range);
            transform.Rotate(z: z, space: Space.Self);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
            {
                player.OnBarrel = onBarrel = true;
            }
        }
    }
}
