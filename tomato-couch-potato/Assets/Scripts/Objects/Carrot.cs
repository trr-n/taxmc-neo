using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class Carrot : Object
    {
        [SerializeField]
        [Tooltip("limit回踏んだらアウト")]
        int limitSteps = 2;

        CarrotFlag flag;
        new BoxCollider2D collider;

        public bool Mendable { get; private set; }
        public float Ratio => (float)flag.Count / limitSteps;

        protected override void Start()
        {
            base.Start();
            sr.sprite = sprites[0];
            flag = transform.GetFromChild<CarrotFlag>();
            flag.Count = 0;
            collider = GetComponent<BoxCollider2D>();
        }

        protected override void Behavior()
        {
            if (!Mendable && flag.Count >= limitSteps)
            {
                effects.TryInstantiate(transform.position);
                Mendable = true;
                sr.enabled = collider.enabled = false;
            }
            sr.sprite = sprites[Ratio < .5f ? 0 : 1];
        }

        public void Mend()
        {
            Mendable = false;
            flag.Count = 0;
            sr.enabled = collider.enabled = true;
        }
    }
}
