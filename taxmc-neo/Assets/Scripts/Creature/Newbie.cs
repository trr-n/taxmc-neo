using System.Collections;
using System.Collections.Generic;
using trrne.utils;
using UnityEngine;

namespace trrne.Game
{
    [RequireComponent(typeof(Health))]
    public class Newbie : Enemy
    {
        [SerializeField]
        GameObject[] dyingFx;

        readonly float distance = 1.5f;
        (Ray ray, RaycastHit2D hit) horizon, vertical;
        readonly int layers = Constant.Layers.Player | Constant.Layers.Ground;

        Player player;
        Health health;

        const float BaseSpeed = 2f;
        float speed;

        void Start()
        {
            player = Gobject.GetWithTag<Player>(Constant.Tags.Player);
            health = GetComponent<Health>();

            speed = Rand.Int(max: 2) switch
            {
                1 => -BaseSpeed * 2,
                _ => BaseSpeed
            };
        }

        void Update()
        {
            Move();
            DetectPlayer();
            Die();
        }

        protected override void DetectPlayer()
        {
            horizon.ray = new(transform.position - (Coordinate.x * distance / 2), transform.right);
            horizon.hit = Physics2D.Raycast(horizon.ray.origin, horizon.ray.direction, distance, layers);

            if (horizon.hit)
            {
                switch (horizon.hit.GetLayer())
                {
                    case Constant.Layers.Player:
                        player.Die();
                        break;

                    case Constant.Layers.Ground:
                    default:
                        // 壁にあたったら進行方向を反転
                        speed *= -1;
                        break;

                }
            }

            vertical.ray = new(transform.position + (distance * Coordinate.y / 2), transform.up);
            vertical.hit = Physics2D.Raycast(vertical.ray.origin, vertical.ray.direction, distance, layers);

            if (vertical.hit) { health.Fluctuation(-1); }

            Debug.DrawRay(horizon.ray.origin, horizon.ray.direction * distance, Color.red);
            Debug.DrawRay(vertical.ray.origin, vertical.ray.direction * distance, Color.blue);
        }

        protected override void Die()
        {
            if (!health.isZero) { return; }

            if (dyingFx.Length > 0)
            {
                dyingFx.Generate(transform.position);
            }
        }

        protected override void Move()
        {
            if (!enable) { return; }

            transform.Translate(Time.deltaTime * speed * Coordinate.x);
        }
    }
}
