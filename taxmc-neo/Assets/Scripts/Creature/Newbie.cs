using Cysharp.Threading.Tasks;
using trrne.utils;
using UnityEngine;

namespace trrne.Game
{
    // [RequireComponent(typeof(Health))]
    public class Newbie : Enemy
    {
        public enum StartFacing { Left, Right }
        [SerializeField]
        StartFacing facing = StartFacing.Right;

        readonly float distance = 1.5f;
        (Ray ray, RaycastHit2D hit) horizon, vertical;
        readonly int layers = Constant.Layers.Player | Constant.Layers.Ground;

        Player player;
        // Health health;

        (float basis, float real) speed = (2f, 0);

        void Start()
        {
            player = Gobject.GetWithTag<Player>(Constant.Tags.Player);
            // health = GetComponent<Health>();

            speed.real = Rand.Int(max: 2) switch
            {
                1 => -speed.basis * 2,
                _ => speed.basis
            };
        }

        protected override async void Behavior()
        {
            horizon.ray = new(transform.position - (Coordinate.x * distance / 2), transform.right);
            horizon.hit = Physics2D.Raycast(horizon.ray.origin, horizon.ray.direction, distance, layers);

            if (horizon.hit)
            {
                switch (horizon.hit.GetLayer())
                {
                    // プレイヤーにあたったら56す
                    case Constant.Layers.Player:
                        await player.Die();
                        break;

                    // プレイヤー以外にあたったら進行方向を反転
                    // case Constant.Layers.Ground:
                    default:
                        speed.real *= -1;
                        break;
                }
            }

            vertical.ray = new(transform.position + (distance * Coordinate.y / 2), transform.up);
            vertical.hit = Physics2D.Raycast(vertical.ray.origin, vertical.ray.direction, distance, layers);

            // プレイヤーに踏まれたら死
            if (vertical.hit && vertical.hit.Compare(Constant.Tags.Player))
            {
                Die();
            }

#if DEBUG
            Debug.DrawRay(horizon.ray.origin, horizon.ray.direction * distance, Color.red);
            Debug.DrawRay(vertical.ray.origin, vertical.ray.direction * distance, Color.blue);
#endif
        }

        protected override async void Die()
        {
            // エフェクト生成
            dieFX.Generate(transform.position);

            // すこーし待機
            await UniTask.WaitForSeconds(0.1f);

            // オブジェクト破壊
            Destroy(gameObject);
        }

        protected override void Move()
        {
            transform.Translate(Time.deltaTime * speed.real * Coordinate.x);
        }
    }
}
