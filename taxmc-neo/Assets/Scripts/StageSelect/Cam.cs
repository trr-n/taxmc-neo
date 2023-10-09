using trrne.Bag;
using UnityEngine;

namespace trrne.Body.Select
{
    public class Cam : MonoBehaviour
    {
        [SerializeField]
        Vector2 leftUp, rightBottom;

        GameObject player;

        void Start()
        {
            player = Gobject.Find(Constant.Tags.Player);
        }

        void Update()
        {
            Follow();
        }

        void Follow()
        {
            // Clamp
            // transform.position = new(
            //     x: Mathf.Clamp(transform.position.x, rightBottom.x, leftUp.x),
            //     y: Mathf.Clamp(transform.position.y, rightBottom.y, leftUp.y)
            // );

            transform.SetPosition(player.transform.position + Vector100.z * transform.position.z);
        }
    }
}
