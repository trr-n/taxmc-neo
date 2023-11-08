using UnityEngine;
using trrne.Brain;
using trrne.Box;

namespace trrne.Core
{
    public class Clear : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D info)
        {
            if (info.CompareTag(Constant.Tags.Player) && info.TryGet(out Player _))
            {
                Recorder.Instance.Clear();
            }
        }
    }
}
