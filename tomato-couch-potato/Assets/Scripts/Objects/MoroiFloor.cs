using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class MoroiFloor : Object
    {
        [SerializeField]
        float landingToleranceTime = 2f;

        bool isLanding = false;
        float timer = 0f;

        Vector2 hitboxSize => new(size.x * 0.9f, size.y * 0.5f);

        BoxCollider2D hitbox;

        public bool Mendable { get; private set; }

        protected override void Start()
        {
            base.Start();
            hitbox = GetComponent<BoxCollider2D>();
        }

        protected override void Behavior()
        {
            IncrementLandingTimer();
            DestroyMe();
        }

        void IncrementLandingTimer()
        {
            if (isLanding)
            {
                print("moroyuka landing.");
                timer += Time.deltaTime;
                return;
            }
            timer = 0f;
        }

        void DestroyMe()
        {
            if (timer < landingToleranceTime)
            {
                return;
            }
            timer = 0f;
            sr.enabled = hitbox.enabled = false;
            Mendable = true;
        }

        public void Mend()
        {
            sr.enabled = hitbox.enabled = true;
            Mendable = false;
        }

        void OnCollisionEnter2D(Collision2D collisionInfo)
        {
            if (collisionInfo.TryGetComponent(out Player _))
            {
                isLanding = true;
            }
        }

        void OnCollisionExit2D(Collision2D collisionInfo)
        {
            if (collisionInfo.TryGetComponent(out Player _))
            {
                isLanding = false;
            }
        }
    }
}
