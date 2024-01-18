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
        float range;

        bool inBarrel = false;
        float initZ;

        float z = 10;

        Rigidbody2D playerRB = null;

        protected override void Start()
        {
            base.Start();
            initZ = transform.eulerAngles.z;
        }

        protected override void Behavior()
        {
            if (!inBarrel)
            {
                return;
            }

            // 樽を回転させてキーが押された時点の角度に合わせて飛ばす
            transform.Rotate(z: Time.deltaTime * z, space: Space.Self);
            if (playerRB != null && Inputs.Down(Constant.Keys.JUMP))
            {
                playerRB = null;
                playerRB.gameObject.GetComponent<Player>().BarrelProcess(false);
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
            {
                player.BarrelProcess(inBarrel = true);
                player.transform.SetPosition(transform);
                playerRB = player.GetComponent<Rigidbody2D>();
            }
        }
    }
}
