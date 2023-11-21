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
        float teleportSpeed = 3f;

        [SerializeField]
        float frameAlpha = .75f;

        const float SpeedRange = 30;

        [SerializeField]
        GameObject[] frames;
        float[] speeds;
        float myspeed;
        bool warping = false;
        int children;

        protected override void Start()
        {
            base.Start();

            // パーティクルを除くため-1
            children = frames.Length;

            speeds = new float[children];
            for (int i = 0; i < children; i++)
            {
                frames[i].GetComponent<SpriteRenderer>().SetAlpha(frameAlpha);
                speeds[i] = Randoms._(-SpeedRange, SpeedRange);
            }
            myspeed = Randoms._(-SpeedRange, SpeedRange);
        }

        protected override void Behavior()
        {
#if DEBUG
            Debug.DrawLine(transform.position, portalGoal.Goal); // 目的地まで線を引く
#endif

            for (int i = 0; i < children; i++)
                // フレームを回転させる
                frames[i].transform.Rotate(Time.deltaTime * speeds[i] * Coordinate.V3Z);

            // ついでに中心も回転させる
            transform.Rotate(Time.deltaTime * myspeed * Coordinate.V3Z);
        }

        void OnTriggerEnter2D(Collider2D info)
        {
            if (warping && !info.CompareTag(Constant.Tags.Player))
                return;

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
