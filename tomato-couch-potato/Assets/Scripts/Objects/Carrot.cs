using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class Carrot : Object, IMendable
    {
        [SerializeField]
        [Tooltip("limit回踏んだらアウト")]
        int limitSteps = 2;

        CarrotFlag flag;
        BoxCollider2D hitbox;

        public bool Mendable { get; private set; }
        public float Ratio => (float)flag.Count / limitSteps;

        protected override void Start()
        {
            base.Start();

            sr.sprite = sprites[0];
            flag = transform.GetFromChild<CarrotFlag>();
            hitbox = GetComponent<BoxCollider2D>();

            flag.ResetCount();
        }

        protected override void Behavior()
        {
            if (!Mendable && flag.Count >= limitSteps)
            {
                effects.TryInstantiate(transform.position);
                Mendable = true;
                sr.enabled = hitbox.enabled = false;
            }
            sr.sprite = sprites[Ratio < .5f ? 0 : 1];
        }

        public void Mend()
        {
            Mendable = false;
            flag.ResetCount();
            sr.enabled = hitbox.enabled = true;
        }
    }
}
