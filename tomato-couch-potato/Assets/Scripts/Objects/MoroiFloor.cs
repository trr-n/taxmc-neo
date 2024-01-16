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

        void Awake()
        {
            landingTime = landingToleranceTime;
        }

        protected override void Start()
        {
            base.Start();

            hitbox = GetComponent<BoxCollider2D>();
            player = Gobject.GetWithTag<Player>(Constant.Tags.PLAYER);
        }

        protected override void Behavior()
        {
            DecrementLandingTimer();
            DestroyMe();
        }

        void LateUpdate()
        {
            // 生きていて乗っていたら
            if (!player.IsDying && isLanding)
            {
                var colorValue = landingTime / landingToleranceTime / 360 * 100;
                sr.color = Color.HSVToRGB(colorValue, 1, 1);
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
                sr.enabled = hitbox.enabled = false;
                Mendable = true;
            }
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
                sr.color = Color.white;
                isLanding = false;
            }
        }
    }
}
