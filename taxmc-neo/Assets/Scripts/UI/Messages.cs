using UnityEngine;
using trrne.WisdomTeeth;

namespace trrne.Body
{
    public class Messages : MonoBehaviour
    {
        GameObject[] msgs;

        void Start()
        {
            msgs = new GameObject[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                msgs[i] = transform.GetChildObject(i);
            }
        }
    }
}
