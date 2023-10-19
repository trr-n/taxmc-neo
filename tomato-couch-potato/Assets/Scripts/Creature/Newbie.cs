using Cysharp.Threading.Tasks;
using Chickenen.Pancreas;
using UnityEngine;

namespace Chickenen.Heart
{
    public class Newbie : Creature
    {
        public enum StartFacing { Left, Right, Random }
        [SerializeField]
        StartFacing facing = StartFacing.Right;

        (Ray ray, RaycastHit2D hit) horizon, top, bottom;
        readonly (float distance, int detect) layer = (1.2f, Constant.Layers.Player | Constant.Layers.Ground);

        Player player;

        bool dying = false;

        (float basis, float real) speed = (2f, 0);

        protected override void Start()
        {
            player = Gobject.GetWithTag<Player>(Constant.Tags.Player);

            speed.real = facing switch
            {
                StartFacing.Left => -speed.basis,
                StartFacing.Right => speed.basis,
                StartFacing.Random or _ => Randoms.Int(max: 2) switch
                {
                    1 => -speed.basis,
                    _ => speed.basis
                }
            };
        }

        protected override async void Behavior()
        {
            horizon.ray = new(transform.position - (Vector100.X * layer.distance / 2), transform.right);
            horizon.hit = Physics2D.Raycast(horizon.ray.origin, horizon.ray.direction, layer.distance, layer.detect);

            if (horizon.hit)
            {
                switch (horizon.hit.GetLayer())
                {
                    // プレイヤーにあたったらDie()
                    case Constant.Layers.Player:
                        await player.Die();
                        break;

                    // プレイヤー以外にあたったら進行方向を反転
                    default:
                        speed.real *= -1;
                        break;
                }
            }

            Vector2 rayconf = transform.position + (layer.distance * Vector100.Y / 2);
            top.ray = new(rayconf, transform.up);
            top.hit = Physics2D.Raycast(top.ray.origin, top.ray.direction, layer.distance, layer.detect);

            if (top.hit && top.hit.CompareTag(Constant.Tags.Player))
            {
                await Die();
            }

            bottom.ray = new(rayconf, -transform.up);
            bottom.hit = Physics2D.Raycast(bottom.ray.origin, bottom.ray.direction, layer.distance, layer.detect);

            if (bottom.hit && bottom.hit.CompareTag(Constant.Tags.Player))
            {
                await bottom.hit.Get<Player>().Die();
            }
        }

        public override async UniTask Die()
        {
            if (dying) { return; }
            dying = true;

            // TODO エフェクト生成
            // diefx.Generate(transform.position);

            // すこーし待機
            await UniTask.WaitForSeconds(0.1f);

            // オブジェクト破壊
            Destroy(gameObject);
        }

        protected override void Move()
        {
            transform.Translate(Time.deltaTime * speed.real * Vector100.X);
        }
    }
}