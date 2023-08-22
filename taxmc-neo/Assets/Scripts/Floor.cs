using System.Collections;
using System.Collections.Generic;
using Self.Utils;
using UnityEngine;

namespace Self.Game
{
    public class Floor : MonoBehaviour
    {
        [SerializeField]
        Sprite sprite;

        [SerializeField]
        float repetiteSpeed;

        [SerializeField]
        float repetiteRange;

        public enum MovingStyle
        {
            Stopped, Horizontal, Vertical
        }
        [SerializeField]
        MovingStyle style = MovingStyle.Stopped;

        Vector3 basePos;

        SpriteRenderer selfSr;

        void Start()
        {
            basePos = transform.position;

            selfSr = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            Move();
        }

        void Move()
        {
            switch (style)
            {
                case MovingStyle.Stopped: break;

                case MovingStyle.Horizontal:
                    var hmin = basePos - Coordinate.X * repetiteRange;
                    var hmax = basePos + Coordinate.X * repetiteRange;

                    if (transform.position.x < hmin.x || transform.position.x > hmax.x)
                    {
                        repetiteSpeed *= -1;
                    }
                    transform.Translate(Time.deltaTime * repetiteSpeed * Coordinate.X, Space.World);
                    break;

                case MovingStyle.Vertical:
                    var vmin = basePos - Coordinate.X * repetiteRange;
                    var vmax = basePos + Coordinate.X * repetiteRange;

                    if (transform.position.y < vmin.y || transform.position.y > vmax.y)
                    {
                        repetiteSpeed *= -1;
                    }
                    transform.Translate(Time.deltaTime * repetiteSpeed * Coordinate.Y, Space.World);
                    break;
            }
        }
    }
}
