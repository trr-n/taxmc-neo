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

        // (Ray ray, RaycastHit2D hit) left, right;
        (Ray ray, RaycastHit2D hit)[] horizon = new (Ray ray, RaycastHit2D hit)[2];
        (Ray ray, RaycastHit2D hit) top, bottom;
        float length, offset, originOffset, half;
        const int DetectLayers = Config.Layers.Player | Config.Layers.Jumpable;


        Player player;

        bool dying = false, hit = false;

        protected override void Start()
        {
            Enable = true;
            player = Gobject.GetWithTag<Player>(Config.Tags.Player);

            BoxCollider2D c = GetComponent<BoxCollider2D>();
            offset = c.size.x * 1.2f;
            length = c.size.x * 0.8f;
            originOffset = (c.size.x - length) * 2;
            half = c.size.x * 0.5f;
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

            // await Horizon();
            await Horizon2();
            // await Top();
            // await Bottom();
        }

        // async UniTask Horizon()
        // {
        //     // TODO 超コードクローン
        //     left.ray = new(
        //         origin: transform.position + new Vector3(-offset / 2, -originOffset),
        //         direction: transform.up
        //     );
        //     left.hit = Gobject.Raycast(left.ray, length, DetectLayers);
        //     left.ray.DrawRay(length, Color.red);

        //     right.ray = new(
        //         origin: transform.position + new Vector3(offset / 2, -originOffset),
        //         direction: transform.up
        //     );
        //     right.hit = Gobject.Raycast(right.ray, length, DetectLayers);
        //     right.ray.DrawRay(length, Color.blue);

        //     if (!left.hit || !right.hit)
        //     {
        //         return;
        //     }

        //     switch (left.hit.GetLayer() | right.hit.GetLayer())
        //     {
        //         case Config.Layers.Player:
        //             await player.Die();
        //             break;
        //         default:
        //             moveDirection *= -1;
        //             break;
        //     }
        // }

        async UniTask Horizon2()
        {
            for (int i = 0, j = -1; j < 2; i++, j += 2)
            {
                horizon[i].ray = new(
                    origin: transform.position + new Vector3(-offset * j / 2, -originOffset),
                    direction: transform.up
                );
                horizon[i].hit = Gobject.Raycast(horizon[i].ray, length, DetectLayers);
                horizon[i].ray.DrawRay(length, Color.red);

                if (!horizon[i].hit)
                {
                    continue;
                }

                switch (horizon[i].hit.GetLayer())
                {
                    case Config.Layers.Player:
                        await player.Die();
                        break;
                    default:
                        direction *= -1;
                        break;
                }
            }
        }

        // async UniTask Top()
        // {
        //     top.ray = new(transform.position, transform.up);
        //     top.hit = Physics2D.Raycast(
        //         origin: top.ray.origin,
        //         direction: top.ray.direction,
        //         distance: hitbox.size.y,
        //         layerMask: hitbox.detect
        //     );
        //     top.ray.DrawRay(hitbox.size.y, Color.green);
        //     if (top.hit && top.hit.TryGetComponent(out Player _))
        //     {
        //         hit = true;
        //         await Die();
        //     }
        // }

        // async UniTask Bottom()
        // {
        //     bottom.ray = new(transform.position, -transform.up);
        //     bottom.hit = Physics2D.Raycast(
        //         origin: bottom.ray.origin,
        //         direction: bottom.ray.direction,
        //         distance: hitbox.size.y,
        //         layerMask: hitbox.detect
        //     );
        //     bottom.ray.DrawRay(hitbox.size.y, Color.blue);
        //     if (!hit && bottom.hit && bottom.hit.TryGetComponent(out Player player))
        //     {
        //         await player.Die();
        //     }
        // }

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
        => transform.Translate(Time.deltaTime * direction * Coordinate.V2X);
    }
}