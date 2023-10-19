using Chickenen.Pancreas;
using UnityEngine;

namespace Chickenen.Heart
{
    public class AirFlow : MonoBehaviour
    {
        Fan fan;

        float distance;

        void Start()
        {
            fan = transform.GetFromParent<Fan>();
        }

        void OnTriggerStay2D(Collider2D info)
        {
            if (info.CompareBoth(Constant.Layers.Player, Constant.Tags.Player)
                && info.TryGet(out Rigidbody2D rb))
            {
                distance = Vector2.Distance(transform.position, info.Position2());
                rb.velocity += distance * fan.power * Time.fixedDeltaTime * Vector100.Y2D;
            }
        }
    }
}