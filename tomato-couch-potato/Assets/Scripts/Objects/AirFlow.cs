using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class AirFlow : MonoBehaviour
    {
        Fan fan;

        void Start()
        {
            fan = transform.GetComponentFromParent<Fan>();
        }

        void OnTriggerStay2D(Collider2D info)
        {
            if (info.CompareBoth(Constant.Layers.Player, Constant.Tags.Player)
                && info.TryGetComponent(out Rigidbody2D rb))
            {
                float distance = Vector2.Distance(transform.position, info.Position2());
                rb.velocity += distance * fan.power * Time.fixedDeltaTime * Coordinate.V2Y;
            }
        }
    }
}