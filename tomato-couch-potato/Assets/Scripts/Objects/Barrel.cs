using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class Barrel : Object
    {
        [SerializeField]
        float power = 50f;

        [SerializeField]
        [Tooltip("回転角")]
        float rotRange = 0;

        [SerializeField]
        float rotSpeed = 10;

        [SerializeField]
        bool isRotate = false;

        bool inBarrel = false;
        float initZ = 0;

        Player player = null;

        protected override void Start()
        {
            base.Start();
            initZ = transform.localEulerAngles.z;
        }

        protected override void Behavior()
        {
            if (isRotate && rotRange != 0)
            {
                var z = Mathf.Sin(rotSpeed * Time.time) * (rotRange / 2) + initZ;
                transform.rotation = Quaternion.Euler(0, 0, z);
            }

            if (inBarrel)
            {
                player.transform.position = transform.position;
                // 樽を回転させてキーが押された時点の角度に合わせて飛ばす
                if (player != null && Inputs.Down(Constant.Keys.JUMP))
                {
                    player.BarrelProcess(false);
                    player.GetComponent<Rigidbody2D>().velocity += power * (Vector2)transform.up;
                    inBarrel = false;
                    player.isAfterBarrel = true;
                    player = null;
                }
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player) && !player.IsDying)
            {
                player.BarrelProcess(inBarrel = true);
                player.transform.position = transform.position;
                this.player = player;
            }
        }
    }
}
