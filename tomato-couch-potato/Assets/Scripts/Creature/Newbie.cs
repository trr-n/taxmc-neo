using Cysharp.Threading.Tasks;
using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class Newbie : Creature, ICreature
    {
        public enum Facing
        {
            Fixed,
            Left,
            Right,
            Random
        }

        public Facing facing = Facing.Fixed;

        [SerializeField]
        float speed = 2f;
        float direction = 0;

        (Ray ray, RaycastHit2D hit) horizon, top, bottom;
        readonly (float size, int detect) hitbox = (1.2f, Config.Layers.Player | Config.Layers.Jumpable);

        Player player;

        bool dying = false, hit = false;

        protected override void Start()
        {
            player = Gobject.GetWithTag<Player>(Config.Tags.Player);
        }

        void SetFacing(Facing facing)
        {
            this.facing = facing;
            direction = facing switch
            {
                Facing.Fixed => 0,
                Facing.Left => -speed,
                Facing.Right => speed,
                Facing.Random or _ => Rnd.Int32(max: 1) switch
                {
                    0 => -speed,
                    1 or _ => speed
                }
            };
        }

        protected override async void Behavior()
        {
            SetFacing(facing);

            Vector3 conf = hitbox.size * Coordinate.V3Y / 2;
            await Horizon(conf);
            await Top(conf);
            await Bottom(conf);
        }

        async UniTask Horizon(Vector3 conf)
        {
            horizon.ray = new(transform.position - conf, transform.right);
            horizon.hit = Physics2D.Raycast(
                origin: horizon.ray.origin,
                direction: horizon.ray.direction,
                distance: hitbox.size,
                layerMask: hitbox.detect
            );
            if (!horizon.hit)
            {
                return;
            }

            switch (horizon.hit.GetLayer())
            {
                case Config.Layers.Player:
                    await player.Die();
                    break;
                default:
                    direction *= -1;
                    break;
            }
        }

        async UniTask Top(Vector3 conf)
        {
            top.ray = new(transform.position + conf, transform.up);
            top.hit = Physics2D.Raycast(
                origin: top.ray.origin,
                direction: top.ray.direction,
                distance: hitbox.size,
                layerMask: hitbox.detect
            );
            if (top.hit && top.hit.TryGetComponent(out Player _))
            {
                hit = true;
                await Die();
            }
        }

        async UniTask Bottom(Vector3 conf)
        {
            bottom.ray = new(transform.position + conf, -transform.up);
            bottom.hit = Physics2D.Raycast(
                origin: bottom.ray.origin,
                direction: bottom.ray.direction,
                distance: hitbox.size,
                layerMask: hitbox.detect
            );
            if (!hit && bottom.hit && bottom.hit.TryGetComponent(out Player player))
            {
                await player.Die();
            }
        }

        public override async UniTask Die()
        {
            if (dying)
            {
                return;
            }

            dying = true;
            diefx.TryInstantiate(transform.position);
            await UniTask.WaitForSeconds(0.1f);

            Destroy(gameObject);
        }

        protected override void Movement()
        {
            // print($"{name} is moving!");
            transform.Translate(Time.deltaTime * direction * Coordinate.V2X);
        }
    }
}