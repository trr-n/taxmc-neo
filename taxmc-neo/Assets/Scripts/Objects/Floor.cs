using System.Collections;
using System.Collections.Generic;
using Self.Utils;
using UnityEngine;

namespace Self.Game
{
    public class Floor : MonoBehaviour
    {
        [SerializeField]
        Sprite[] sprites;

        [SerializeField]
        float repetiteSpeed;

        [SerializeField]
        float repetiteRange;

        public enum MovingStyle
        {
            Fixed, Horizontal, Vertical
        }
        [SerializeField]
        MovingStyle style = MovingStyle.Fixed;

        Vector3 basePos;

        SpriteRenderer sr;

        void Start()
        {
            // sr = GetComponent<SpriteRenderer>();
            basePos = transform.position;
        }

        void Update()
        {
            Move();
        }

        void Move()
        {
            switch (style)
            {
                case MovingStyle.Fixed: break;

                case MovingStyle.Horizontal:
                    var x = Coordinate.X * repetiteRange;
                    if (transform.position.x < (basePos - x).x || transform.position.x > (basePos + x).x)
                    {
                        repetiteSpeed *= -1;
                    }
                    transform.Translate(Time.deltaTime * repetiteSpeed * Coordinate.X, Space.World);
                    break;

                case MovingStyle.Vertical:
                    var y = Coordinate.Y * repetiteRange;
                    if (transform.position.y < (basePos - y).y || transform.position.y > (basePos + y).y)
                    {
                        repetiteSpeed *= -1;
                    }
                    transform.Translate(Time.deltaTime * repetiteSpeed * Coordinate.Y, Space.World);
                    break;
            }
        }
    }
}
