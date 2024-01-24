using System;
using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class MoroiFloor : Object, IMendable
    {
        [SerializeField]
        float landingToleranceTime = 2f;

        bool isLanding = false;
        float landingTime = float.MaxValue;

        BoxCollider2D hitbox;

        public bool Mendable { get; private set; }

        Player player;

        SpriteRenderer[] childrenSrs;

        void Awake()
        {
            landingTime = landingToleranceTime;
        }

        protected override void Start()
        {
            base.Start();
            isAnimate = false;

            hitbox = GetComponent<BoxCollider2D>();
            player = Gobject.GetWithTag<Player>(Constant.Tags.PLAYER);
            childrenSrs = transform.GetComponentsInChildren<SpriteRenderer>();
            childrenSrs.ForEach(childSr => childSr.SetColor(Color.white));
            sr.enabled = false;
        }

        protected override void Behavior()
        {
            DecrementLandingTimer();
            DestroyMe();
            sr.enabled = false;
        }

        void LateUpdate()
        {
            if (!player.IsDying && isLanding)
            {
                var ratio = landingTime / landingToleranceTime;
                foreach (var childSr in childrenSrs)
                {
                    childSr.SetColor(new Color(1, ratio, ratio));
                }
            }
        }

        void DecrementLandingTimer()
        {
            if (isLanding)
            {
                landingTime -= Time.deltaTime;
                return;
            }
            landingTime = landingToleranceTime;
        }

        void DestroyMe()
        {
            if (landingTime <= 0)
            {
                landingTime = landingToleranceTime;
                // sr.enabled = false;
                hitbox.enabled = false;
                childrenSrs.ForEach(childSr => childSr.enabled = false);
                Mendable = true;
            }
        }

        public void Mend()
        {
            // sr.enabled = false;
            hitbox.enabled = true;
            childrenSrs.ForEach(childSr =>
            {
                childSr.SetColor(Color.white);
                childSr.enabled = true;
            });
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
                childrenSrs.ForEach(childSr => childSr.SetColor(Color.white));
                isLanding = false;
            }
        }
    }
}
