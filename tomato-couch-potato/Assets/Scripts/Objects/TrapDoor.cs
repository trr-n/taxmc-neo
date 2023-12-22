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

        const float OFFSET = 1e-8f;

        BoxCollider2D hitbox;

        void Start()
        {
            value = transform.eulerAngles;
            hitbox = transform.GetFromChild<BoxCollider2D>(0);
        }

        public override void On()
        {
            var offset = Vec.Z * ((float)amount - OFFSET);
            var rotation = direct switch
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

            transform.DORotate(rotation, speed)
                .OnPlay(() => (!enableColliderOnRotating).If(() => hitbox.enabled = false))
                .OnComplete(() => hitbox.enabled = true);
        }

        public override void Off()
        {
            transform.DORotate(value, speed)
                .OnPlay(() => (!enableColliderOnRotating).If(() => hitbox.enabled = false))
                .OnComplete(() => hitbox.enabled = true);
        }
    }
}

// https://imagingsolution.net/math/rotate-around-point/