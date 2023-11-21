using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class MovingFloorFlag : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D info)
        {
            if (info.CompareTag(Constant.Tags.Player) && info.transform.parent != transform)
                info.transform.parent = transform;
        }

        void OnTriggerExit2D(Collider2D info)
        {
            if (info.CompareTag(Constant.Tags.Player) && info.transform.parent != null)
                info.transform.parent = null;
        }
    }
}
