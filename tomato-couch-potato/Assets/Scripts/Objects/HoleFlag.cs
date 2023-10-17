using trrne.Teeth;
using UnityEngine;

namespace trrne.Body
{
    public class HoleFlag : MonoBehaviour
    {
        public int count { get; set; }

        void OnTriggerEnter2D(Collider2D info)
        {
            if (info.CompareTag(Constant.Tags.Player))
            {
                count++;
            }
        }
    }
}