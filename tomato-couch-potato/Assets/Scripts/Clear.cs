using UnityEngine;
using trrne.Brain;
using trrne.Box;

namespace trrne.Core
{
    public class Clear : MonoBehaviour
    {
        [SerializeField]
        [Header("0: down\n1: up")]
        Sprite[] sprites;

        SpriteRenderer sr;

        void Start()
        {
            sr = GetComponent<SpriteRenderer>();
            sr.sprite = sprites[0];
        }

        void OnTriggerEnter2D(Collider2D info)
        {
            if (info.CompareTag(Constant.Tags.Player) && info.TryGet(out Player _))
            {
                sr.sprite = sprites[1];
                Recorder.Instance.Clear();
            }
        }
    }
}
