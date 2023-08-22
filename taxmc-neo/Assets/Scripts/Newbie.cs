using System.Collections;
using System.Collections.Generic;
using Self.Utils;
using UnityEngine;

namespace Self.Game
{
    public class Newbie : Enemy
    {
        readonly (float horizon, float vertical) distances = (1.5f, 1);
        (Ray ray, RaycastHit2D hit) horizon, vertical;
        readonly int layers = Constant.Layers.Player | Constant.Layers.Ground;

        FryedFlies player;

        float speed;

        void Start()
        {
            player = Gobject.GetWithTag<FryedFlies>(Constant.Tags.Player);
        }

        void Update()
        {
            Move();
            DetectPlayer();
        }

        protected override void DetectPlayer()
        {
            horizon.ray = new(transform.position - (Coordinate.X * distances.horizon / 2), transform.right);
            Debug.DrawRay(horizon.ray.origin, horizon.ray.direction * distances.horizon, Color.red);
            horizon.hit = Physics2D.Raycast(horizon.ray.origin, horizon.ray.direction, distances.horizon, layers);

            if (horizon.hit)
            {
                switch (horizon.hit.GetLayer())
                {
                    case Constant.Layers.Player:
                        player.Die();
                        break;

                    case Constant.Layers.Ground:
                        // 進行方向を反転
                        speed *= -1;
                        break;
                }
            }
        }

        protected override void Move()
        {
            transform.Translate(Time.deltaTime * speed * Coordinate.X);
        }
    }
}
