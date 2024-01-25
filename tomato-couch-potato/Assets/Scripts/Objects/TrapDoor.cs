using trrne.Box;
using DG.Tweening;
using UnityEngine;
using System;

namespace trrne.Core
{
    public class TrapDoor : Gimmick, IGimmick
    {
        public enum RotateDirection
        {
            Left,
            Right,
            Random
        }

        [SerializeField]
        RotateDirection direct;

        public enum RotateAmount
        {
            _90 = 90,
            _180 = 180,
        }

        [SerializeField]
        RotateAmount amount = RotateAmount._90;

        [SerializeField]
        float speed = 0.5f;
        Vector3 value;

        [Tooltip("回転中の当たり判定")]
        [SerializeField]
        bool enableColliderOnRotating = false;

        const float ROTATE_OFFSET = 1e-8f;
        Vector3 offset => new(0, 0, (float)amount - ROTATE_OFFSET);

        BoxCollider2D hitbox;

        protected override void Start()
        {
            base.Start();
            sr.SetSprite(sprites);
            value = transform.eulerAngles;
            hitbox = GetComponent<BoxCollider2D>();
        }

        public override void On()
        {
            var direct = this.direct switch
            {
                RotateDirection.Left => value + offset,
                RotateDirection.Right => value - offset,
                RotateDirection.Random => Rand.Int(max: 1) switch
                {
                    0 => value + offset,
                    _ => value - offset
                },
                _ => throw new Exception()
            };
            transform.DORotate(direct, speed)
                .OnPlay(() =>
                {
                    if (!enableColliderOnRotating)
                    {
                        hitbox.enabled = false;
                    }
                })
                .OnComplete(() => hitbox.enabled = true);
        }

        public override void Off()
        {
            transform.DORotate(value, speed)
                .OnPlay(() =>
                {
                    if (!enableColliderOnRotating)
                    {
                        hitbox.enabled = false;
                    }
                })
                .OnComplete(() => hitbox.enabled = true);
        }

        protected override void Behavior() { }
    }
}

// https://imagingsolution.net/math/rotate-around-point/