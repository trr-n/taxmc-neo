using DG.Tweening;
using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class Portal : Object
    {
        [SerializeField]
        PortalGoal portalGoal;

        [SerializeField]
        float teleportSpeed = 1.5f;

        [SerializeField]
        float framesAlpha = .75f;

        const float ROTATE_SPEED = 30;

        [SerializeField]
        GameObject[] frames;
        float[] speeds;
        float myspeed;
        bool warping = false;
        int children;

        protected override void Start()
        {
            base.Start();

            children = frames.Length;
            speeds = new float[children];
            for (int i = 0; i < children; ++i)
            {
                frames[i].GetComponent<SpriteRenderer>().SetAlpha(framesAlpha);
                speeds[i] = Rand.Float(-ROTATE_SPEED, ROTATE_SPEED);
            }
            myspeed = Rand.Float(-ROTATE_SPEED, ROTATE_SPEED);
        }

        protected override void Behavior()
        {
#if UNITY_EDITOR
            Debug.DrawLine(transform.position, portalGoal.Goal);
#endif
            for (int i = 0; i < children; ++i)
            {
                // frames[i].transform.Rotate(Time.deltaTime * speeds[i] * Vec.Z);
                frames[i].transform.Rotate(Vec.Make3(z: Time.deltaTime * speeds[i]));
            }
            transform.Rotate(z: Time.deltaTime * myspeed);
        }

        void OnTriggerEnter2D(Collider2D info)
        {
            if (warping)
            {
                return;
            }

            if (info.TryGetComponent(out Player player) && !player.IsDying)
            {
                info.transform.DOMove(portalGoal.Goal, teleportSpeed)
                    .SetEase(Ease.OutCubic)
                    .OnPlay(() =>
                    {
                        warping = true;
                        player.IsTeleporting = true;
                        effects.TryInstantiate(transform.position);
                    })
                    .OnComplete(() =>
                    {
                        player.IsTeleporting = false;
                        warping = false;
                    });
            }
        }
    }
}
