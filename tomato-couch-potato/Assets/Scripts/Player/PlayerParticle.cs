using UnityEngine;

namespace trrne.Core
{
    public class PlayerParticle : MonoBehaviour
    {
        [SerializeField]
        [Header("0:mirror\n1:chain\n2:fetters")]
        GameObject[] vfxs;
    }
}
