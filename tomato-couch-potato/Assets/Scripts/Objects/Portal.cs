using DG.Tweening;
using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class Portal : Object
    {
        [SerializeField]
        Vector2 to;

        [SerializeField]
        float teleportSpeed = 3f;

        [SerializeField]
        float frameAlpha = .75f;

        const float SpeedRange = 30;

        GameObject[] frames;
        float[] speeds;
        float myspeed;
        bool warping = false;
        int children;

        protected override void Start()
        {
            base.Start();

            // パーティクルを除くため-1
            children = transform.childCount - 1;

            frames = new GameObject[children];
            speeds = new float[children];
            for (int i = 0; i < children; i++)
            {
                frames[i] = transform.GetChildObject(i);
                frames[i].GetComponent<SpriteRenderer>().SetAlpha(frameAlpha);
                speeds[i] = Randoms.Float(-SpeedRange, SpeedRange);
            }
            myspeed = Randoms.Float(-SpeedRange, SpeedRange);
        }

        protected override void Behavior()
        {
            for (int i = 0; i < children; i++)
            {
                // フレームを回転させる
                frames[i].transform.Rotate(Time.deltaTime * speeds[i] * Vector100.Z);
            }

            // ついでに中心も回転させる
            transform.Rotate(Time.deltaTime * myspeed * Vector100.Z);
        }

        void OnTriggerEnter2D(Collider2D info)
        {
            if (warping && !info.CompareTag(Constant.Tags.Player))
            {
                return;
            }

            if (info.TryGet(out Player player) && !player.IsDieProcessing)
            {
                warping = true;

                effects.TryGenerate(transform.position);

                info.transform.DOMove(to, teleportSpeed).SetEase(Ease.OutCubic)
                    .OnPlay(() => player.IsTeleporting = true)
                    .OnComplete(() => player.IsTeleporting = false);

                warping = false;
            }
        }
    }
}
