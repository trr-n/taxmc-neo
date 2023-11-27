using trrne.Box;
using DG.Tweening;
using UnityEngine;
using System;
using System.Runtime.Serialization;

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

        /// <summary>
        /// 回転してるか
        /// </summary>
        // bool isRotating = false;
        (bool flag, float speed, Vector3 value) rotation = (false, 0.5f, new());

        void Start()
        {
            rotation.value = transform.eulerAngles;
        }

        /// <summary>
        /// ギミックを起動する
        /// </summary>
        public override void On()
        {
            Vector3 offset = Coordinate.V3Z * ((float)amount - 1e-8f);
            Vector3 rotation = direct switch
            {
                RotateDirection.Left => this.rotation.value + offset,
                RotateDirection.Right => this.rotation.value - offset,
                _ => throw new Exception()
            };
            transform.DORotate(rotation, this.rotation.speed)
                .OnPlay(() => this.rotation.flag = true)
                .OnComplete(() => this.rotation.flag = false);
        }

        /// <summary>
        /// 停止する
        /// </summary>
        public override void Off()
        {
            transform.DORotate(rotation.value, rotation.speed)
                .OnPlay(() => rotation.flag = true)
                .OnComplete(() => rotation.flag = false);
        }
    }
}

// https://imagingsolution.net/math/rotate-around-point/