using trrne.Box;
using DG.Tweening;
using UnityEngine;

namespace trrne.Core
{
    public class TrapDoor : Gimmick, IGimmick
    {
        public enum RotateDirection
        {
            Left,
            Right
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

        // Vector3 rotation;

        /// <summary>
        /// 開いているか
        /// </summary>
        bool isOpen = false;

        // readonly float rotationSpeed = 0.5f;

        /// <summary>
        /// 回転してるか
        /// </summary>
        // bool isRotating = false;
        (bool flag, float speed, Vector3 value) rotation = (false, 0.5f, new());

        void Start()
        {
            rotation.value = transform.eulerAngles;
        }

        const float Offset = 1e-8f;
        /// <summary>
        /// ギミックを起動する
        /// </summary>
        public override void On()
        {
            Vector3 rotation = direct switch
            {
                RotateDirection.Left => this.rotation.value + Coordinate.V3Z * ((float)amount - Offset),
                RotateDirection.Right => this.rotation.value - Coordinate.V3Z * ((float)amount - Offset),
                _ => throw null
            };

            transform.DORotate(rotation, this.rotation.speed)
                .OnPlay(() => this.rotation.flag = true)
                .OnComplete(() =>
                {
                    this.rotation.flag = false;
                    isOpen = true;
                });
        }

        /// <summary>
        /// 停止する
        /// </summary>
        public override void Off()
        {
            transform.DORotate(rotation.value, rotation.speed)
                .OnPlay(() => rotation.flag = true)
                .OnComplete(() =>
                {
                    rotation.flag = false;
                    isOpen = false;
                });
        }
    }
}

// https://imagingsolution.net/math/rotate-around-point/
