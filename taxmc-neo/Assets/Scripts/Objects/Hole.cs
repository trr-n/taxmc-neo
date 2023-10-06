using trrne.Bag;
using UnityEngine;

namespace trrne.Body
{
    public class Hole : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("limit回踏んだらアウト")]
        int limit = 2;

        HoleFlag flag;

        SpriteRenderer sr;
        new BoxCollider2D collider;

        bool breaking = false;
        public bool isBreaking => breaking;

        void Start()
        {
            flag = transform.GetFromChild<HoleFlag>();
            flag.count = 0;

            sr = GetComponent<SpriteRenderer>();
            collider = GetComponent<BoxCollider2D>();
        }

        void Update()
        {
            if (!breaking && flag.count >= limit)
            {
                breaking = true;
                sr.enabled = false;
                collider.enabled = false;
            }

            // sr.color = colors[flag.count];
        }

        public void Mending()
        {
            print("修繕中");
            breaking = false;
            flag.count = 0;

            sr.enabled = true;
            collider.enabled = true;
        }
    }
}
