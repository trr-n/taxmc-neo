using UnityEngine;
using trrne.Bag;
using System.Collections;

namespace trrne.Body
{
    public class Blower : Objectt
    {
        [SerializeField]
        Sprite[] blowerSprites, flowSprites;

        [SerializeField, Tooltip("風量")]
        float pressure = 500f;

        GameObject flowObj;
        SpriteRenderer flowSr;

        protected override void Start()
        {
            base.Start();

            flowObj = transform.GetChild();
            flowSr = flowObj.GetComponent<SpriteRenderer>();
        }

        protected override void Behavior()
        {
            if (Gobject.BoxCast2D(out var hit, flowObj.Position(), flowSr.bounds.size, Constant.Layers.Player))
            {
                hit.Get<Rigidbody2D>().velocity += pressure * (Vector2)Coordinate.y;
            }
        }
    }
}
