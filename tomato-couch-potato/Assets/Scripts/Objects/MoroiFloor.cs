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
            DetectPlayer();
            IncrementLandingTimer();
            DestroyMe();
        }

        void IncrementLandingTimer()
        {
            if (isLanding)
            {
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
            sr.enabled = false;
            Mendable = true;
            // Destroy(gameObject);
        }

        void Mend()
        {
            sr.enabled = true;
            Mendable = false;
        }

        void DetectPlayer()
        {
            isLanding = Gobject.Boxcast(
                out var hit, transform.position, hitboxSize, Config.Layers.PLAYER)
                && hit.TryGetComponent<Player>(out _);
        }
    }
}
