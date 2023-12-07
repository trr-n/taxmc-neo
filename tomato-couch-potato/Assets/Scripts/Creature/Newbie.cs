using System.Collections;
using Cysharp.Threading.Tasks;
using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    // TODO try/catchでエラー捻り潰しまくってる
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

        bool hitHorizon = false, hitVertical = false;

        Player player;

        bool dying = false, isDestroy = false;

        protected override void Start()
        {
            Enable = true;

            player = Gobject.GetWithTag<Player>(Config.Tags.Player);

            var hitbox = GetComponent<BoxCollider2D>();
            offset = hitbox.size.x * 1.2f;
            length = hitbox.size.x * 0.8f;
            originOffset = (hitbox.size.x - length) * 2;

            SetFacing(facing);
        }

        public void SetFacing(Facing facing)
        {
            this.facing = facing;
            direction = facing switch
            {
                Facing.None => 0,
                Facing.Left => -speed,
                Facing.Right => speed,
                Facing.Random or _ => Rnd.Int(max: 1) switch
                {
                    0 => -speed,
                    1 or _ => speed
                }
            };
        }

        protected override async void Behavior()
        {
            await Horizon(-offset, -originOffset);  // Player: Dead     Self: Alive
            await Top(-originOffset, offset);       // Player: Alive    Self: Dead
            await Bottom(-originOffset, -offset);   // Player: Dead     Self: Alive
        }

        async UniTask Horizon(float originX, float originY)
        {
            try
            {
                if (this == null || hitVertical)
                {
                    return;
                }

                for (int i = 0, j = -1; j < 2; i++, j += 2)
                {
                    horizon[i].ray = new(
                        transform.position + new Vector3(originX * j / 2, originY),
                        transform.up
                    );
                    horizon[i].hit = horizon[i].ray.Raycast(length, DetectLayers);
                    horizon[i].ray.DrawRay(length, Color.green);

                    if (!horizon[i].hit)
                    {
                        continue;
                    }

                    switch (horizon[i].hit.GetLayer())
                    {
                        case Config.Layers.Player:
                            if (!isDestroy || !player.IsDying)
                            {
                                await player.Die();
                                StartCoroutine(HitHorizonChanger());
                            }
                            break;
                        default:
                            direction *= -1;
                            break;
                    }
                }
            }
            catch (MissingReferenceException) { }
        }

        IEnumerator HitHorizonChanger()
        {
            hitHorizon = true;
            yield return new WaitForSeconds(Time.deltaTime * 2);
            hitHorizon = false;
        }

        async UniTask Top(float originX, float originY)
        {
            try
            {
                if (this == null)
                {
                    return;
                }

                top.ray = new(
                    transform.position + new Vector3(originX, originY / 2),
                    transform.right
                );
                top.hit = top.ray.Raycast(length, DetectLayers);
                top.ray.DrawRay(length, Color.red);

                if (!isDestroy && !hitHorizon
                    && top.hit && top.hit.TryGetComponent(out Player _)
                    && !player.IsDying)
                {
                    hitVertical = true;
                    isDestroy = true;
                    await Die();
                }
            }
            catch (MissingReferenceException) { }
        }

        async UniTask Bottom(float originX, float originY)
        {
            try
            {
                if (this == null)
                {
                    return;
                }

                bottom.ray = new(
                    transform.position + new Vector3(originX, originY / 2),
                    transform.right
                );
                bottom.hit = bottom.ray.Raycast(length, DetectLayers);
                bottom.ray.DrawRay(length, Color.blue);

                if (bottom.hit && bottom.hit.TryGetComponent(out Player _))
                {
                    isDestroy = true;
                    await player.Die();
                }
            }
            catch (MissingReferenceException) { }
        }

        public override async UniTask Die()
        {
            try
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
            catch (MissingReferenceException) { }
        }

        protected override void Movement()
        {
            var dir = Time.deltaTime * direction * Vec.VX;
            // print(dir);
            transform.Translate(dir);
        }
    }
}