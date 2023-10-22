using DG.Tweening;
using Chickenen.Pancreas;
using UnityEngine;

namespace Chickenen.Heart
{
    public class Portal : Object
    {
        [SerializeField]
        Vector2 to;

        [SerializeField]
        float teleportSpeed = 3f;

        [SerializeField]
        float frameAlpha = .75f;

        readonly float speed_range = 30;

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
                speeds[i] = Randoms.Float(-speed_range, speed_range);
            }
            myspeed = Randoms.Float(-speed_range, speed_range);
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
            if (!warping && info.CompareBoth(Constant.Layers.Player, Constant.Tags.Player))
            {
                // var player = info.Get<Player>();
                if (info.TryGet(out Player player) && !player.IsDieProcessing)
                {
                    warping = true;
                    effects.TryGenerate(transform.position);

                    info.transform.DOMove(to, teleportSpeed)
                        .SetEase(Ease.OutCubic)
                        .OnPlay(() => player.IsTeleporting = true)
                        .OnComplete(() => player.IsTeleporting = false);
                    warping = false;
                }
            }
        }
    }
}
