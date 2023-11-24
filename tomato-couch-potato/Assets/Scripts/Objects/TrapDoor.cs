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

        const float Offset = 1e-8f;
        /// <summary>
        /// ギミックを起動する
        /// </summary>
        public override void On()
        {
            Vector3 rotation = ((float)amount - Offset) * direct switch
            {
                RotateDirection.Left => this.rotation.value + Coordinate.V3Z,
                RotateDirection.Right => this.rotation.value - Coordinate.V3Z,
                RotateDirection.Random or _ => Randoms.Int32(max: Typing.Length2<RotateDirection>()) switch
                {
                    0 => this.rotation.value + Coordinate.V3Z,
                    _ => this.rotation.value - Coordinate.V3Z,
                }
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
