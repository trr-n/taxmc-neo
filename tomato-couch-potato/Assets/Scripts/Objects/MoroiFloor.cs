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

        Vector2 hitbox => new(size.x * 0.9f, size.y * 0.5f);

        protected override void Behavior()
        {
            DetectPlayer();
            AddLandingTimer();
            DestroyMe();
        }

        void AddLandingTimer()
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
            Destroy(gameObject);
        }

        void DetectPlayer()
        {
            isLanding = Gobject.Boxcast(out var hit, transform.position, hitbox, Config.Layers.PLAYER)
                && hit.TryGetComponent<Player>(out _);
        }
    }
}
