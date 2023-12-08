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

        (bool flag, float speed, Vector3 value) rotation = (false, 0.5f, new());

        void Start()
        {
            rotation.value = transform.eulerAngles;
        }

        public override void On()
        {
            var offset = Vec.VZ * ((float)amount - 1e-8f);
            var rotation = direct switch
            {
                RotateDirection.Left => this.rotation.value + offset,
                RotateDirection.Right => this.rotation.value - offset,
                _ => throw new Exception()
            };

            transform.DORotate(rotation, this.rotation.speed)
                .OnPlay(() => this.rotation.flag = true)
                .OnComplete(() => this.rotation.flag = false);
        }

        public override void Off()
        {
            transform.DORotate(rotation.value, rotation.speed)
                .OnPlay(() => rotation.flag = true)
                .OnComplete(() => rotation.flag = false);
        }
    }
}

// https://imagingsolution.net/math/rotate-around-point/