using System.Collections;
using System.Collections.Generic;
using trrne.Utils;
using UnityEngine;

namespace trrne.Game
{
    public class Floor : MonoBehaviour
    {
        [SerializeField]
        Sprite[] sprites;

        [SerializeField]
        float repetiteSpeed;

        [SerializeField]
        float repetiteRange;

        public enum MovingStyle { Fixed, Horizontal, Vertical }
        [SerializeField]
        MovingStyle style = MovingStyle.Fixed;

        Vector3 basePos;

        // SpriteRenderer sr;

        void Start()
        {
            // sr = GetComponent<SpriteRenderer>();
            basePos = transform.position;
        }

        void Update()
        {
            Move();
        }

        /// <summary>
        /// 移動
        /// </summary>
        void Move()
        {
            switch (style)
            {
                case MovingStyle.Fixed: break;

                case MovingStyle.Horizontal:
                    var x = Coordinate.x * repetiteRange;

                    // 可動域を超えたら速度反転
                    if (transform.position.x < (basePos - x).x || transform.position.x > (basePos + x).x)
                    {
                        repetiteSpeed *= -1;
                    }
                    transform.Translate(Time.deltaTime * repetiteSpeed * Coordinate.x, Space.World);
                    break;

                case MovingStyle.Vertical:
                    var y = Coordinate.y * repetiteRange;

                    // 可動域を超えたら速度反転
                    if (transform.position.y < (basePos - y).y || transform.position.y > (basePos + y).y)
                    {
                        repetiteSpeed *= -1;
                    }
                    transform.Translate(Time.deltaTime * repetiteSpeed * Coordinate.y, Space.World);
                    break;
            }
        }
    }
}
