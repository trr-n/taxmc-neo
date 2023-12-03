using Cysharp.Threading.Tasks;
using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class Newbie : Creature, ICreature
    {
        public enum Facing
        {
            None,
            Left,
            Right,
            Random
        }

        [SerializeField]
        Facing facing = Facing.None;

        [SerializeField]
        float speed = 2f;
        float direction = 0;

        readonly (Ray ray, RaycastHit2D hit)[] horizon = { new(), new() };
        (Ray ray, RaycastHit2D hit) top, bottom;
        float length, offset, originOffset;
        const int DetectLayers = Config.Layers.Player | Config.Layers.Jumpable;


        Player player;

        bool dying = false, hit = false;

        protected override void Start()
        {
            Enable = true;
            player = Gobject.GetWithTag<Player>(Config.Tags.Player);

            var hitbox = GetComponent<BoxCollider2D>();
            offset = hitbox.size.x * 1.2f;
            length = hitbox.size.x * 0.8f;
            originOffset = (hitbox.size.x - length) * 2;
        }

        public void SetFacing(Facing facing)
        {
            this.facing = facing;
            direction = facing switch
            {
                Facing.None => 0,
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

            if (player.IsDying)
            {
                return;
            }

            await Horizon(-offset, -originOffset);
            await Top(-originOffset, offset);
            await Bottom(-originOffset, -offset);
        }

        async UniTask Horizon(float originX, float originY)
        {
            for (int i = 0, j = -1; j < 2; i++, j += 2)
            {
                horizon[i].ray = new(
                    transform.position + new Vector3(originX * j / 2, originY),
                    transform.up
                );
                horizon[i].hit = horizon[i].ray.Raycast(length, DetectLayers);
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

        async UniTask Top(float originX, float originY)
        {
            top.ray = new(
                transform.position + new Vector3(originX, originY / 2),
                transform.right
            );
            top.hit = top.ray.Raycast(length, DetectLayers);
            top.ray.DrawRay(length, Surface.Gaming);

            if (top.hit && top.hit.TryGetComponent(out Player _))
            {
                hit = true;
                await Die();
            }
        }

        async UniTask Bottom(float originX, float originY)
        {
            bottom.ray = new(
                transform.position + new Vector3(originX, originY / 2),
                transform.right
            );
            bottom.hit = bottom.ray.Raycast(length, DetectLayers);
            bottom.ray.DrawRay(length, Surface.Gaming);

            if (!hit && bottom.hit && bottom.hit.TryGetComponent(out Player _))
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
        => transform.Translate(Time.deltaTime * direction * Coordinate.V2X);
    }
}