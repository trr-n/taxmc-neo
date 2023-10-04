using UnityEngine;

namespace trrne.Body
{
    public class PadCore : MonoBehaviour
    {
        [SerializeField]
        [Range(0f, 15)]
        float jumpPower = 0.1f;
        public float power => jumpPower;
    }
}
