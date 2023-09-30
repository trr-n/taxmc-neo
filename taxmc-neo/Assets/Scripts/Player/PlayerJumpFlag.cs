using UnityEngine;

namespace trrne.Body
{
    public class PlayerJumpFlag : MonoBehaviour
    {
        bool hit;
        public bool Hit => hit;

        void OnTriggerEnter2D(Collider2D info)
        {
            hit = true;
        }

        void OnTriggerExit2D(Collider2D info)
        {
            hit = false;
        }
    }
}