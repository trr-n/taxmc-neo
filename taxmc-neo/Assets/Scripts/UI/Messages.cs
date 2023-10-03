using UnityEngine;
using trrne.Bag;

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
                msgs[i] = transform.GetChilda(i);
            }
        }
    }
}
