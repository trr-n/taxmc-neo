using Cysharp.Threading.Tasks;
using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class Newbie : Creature, ICreature
    {
        public enum StartFacing
        {
            Left,
            Right,
            Random
        }

        [SerializeField]
        StartFacing facing = StartFacing.Right;

        (Ray ray, RaycastHit2D hit) horizon, top, bottom;
        (float size, int detect) hitbox => (1.2f, Constant.Layers.Player | Constant.Layers.Jumpable);

        Player player;

        bool dying = false;

        (float basis, float real) speed = (2f, 0);

        bool hit = false;

        protected override void Start()
        {
            player = Gobject.GetWithTag<Player>(Constant.Tags.Player);

            speed.real = facing switch
            {
                StartFacing.Left => -speed.basis,
                StartFacing.Right => speed.basis,
                StartFacing.Random or _ => Randoms._(max: 2) switch
                {
                    1 => -speed.basis,
                    _ => speed.basis
                }
            };
        }

        protected override async void Behavior()
        {
            Vector2 configure = transform.position + (hitbox.size * Coordinate.V3Y / 2);
            await Horizon();
            await Top(configure);
            await Bottom(configure);
        }

        async UniTask Horizon()
        {
            horizon.ray = new(transform.position - (Coordinate.V3X * hitbox.size / 2), transform.right);
            horizon.hit = Physics2D.Raycast(horizon.ray.origin, horizon.ray.direction, hitbox.size, hitbox.detect);
            if (!horizon.hit)
                return;
            switch (horizon.hit.GetLayer())
            {
                case Constant.Layers.Player:
                    await player.Die();
                    break;
                default:
                    speed.real *= -1;
                    break;
            }
        }

        async UniTask Top(Vector2 conf)
        {
            top.ray = new(conf, transform.up);
            top.hit = Physics2D.Raycast(top.ray.origin, top.ray.direction, hitbox.size, hitbox.detect);
            if (top.hit && top.hit.TryGetComponent(out Player _))
            {
                hit = true;
                await Die();
            }
        }

        async UniTask Bottom(Vector2 conf)
        {
            bottom.ray = new(conf, -transform.up);
            bottom.hit = Physics2D.Raycast(bottom.ray.origin, bottom.ray.direction, hitbox.size, hitbox.detect);
            if (!hit && bottom.hit && bottom.hit.TryGetComponent(out Player player))
                await player.Die();
        }

        public override async UniTask Die()
        {
            if (dying)
                return;
            dying = true;
            diefx.TryInstantiate(transform.position);
            await UniTask.WaitForSeconds(0.1f);

            // オブジェクト破壊
            Destroy(gameObject);
        }

        protected override void Movement() => transform.Translate(Time.deltaTime * speed.real * Coordinate.V2X);
    }
}