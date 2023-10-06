using Cysharp.Threading.Tasks;
using DG.Tweening;
using trrne.Bag;
using UnityEngine;
using System.Collections;

namespace trrne.Body
{
    public class Portal : Objectt
    {
        [SerializeField]
        Vector2 to;

        [SerializeField]
        float teleportSpeed = 3f;

        [SerializeField]
        float speed_range = 30;

        (GameObject[] frames, float[] speeds) child;
        float myspeed;
        bool warping = false;
        int loop;

        protected override void Start()
        {
            base.Start();

            // パーティクルを除くため-1
            loop = transform.childCount - 1;

            child.frames = new GameObject[loop];
            child.speeds = new float[loop];
            for (int i = 0; i < loop; i++)
            {
                child.frames[i] = transform.GetChilda(i);
                child.speeds[i] = Rand.Float(-speed_range, speed_range);
            }

            myspeed = Rand.Float(-speed_range, speed_range);

            foreach (var frame in child.frames)
            {
                frame.GetComponent<SpriteRenderer>().SetAlpha(0.75f);
            }
        }

        protected override void Behavior()
        {
            for (int i = 0; i < loop; i++)
            {
                // フレームを回転させる
                child.frames[i].transform.Rotate(Time.deltaTime * child.speeds[i] * Coordinate.z);
            }

            // ついでに中心も回転させる
            transform.Rotate(Time.deltaTime * myspeed * Coordinate.z);
        }

        void OnTriggerEnter2D(Collider2D info)
        {
            if (!warping && info.CompareBoth(Constant.Layers.Player, Constant.Tags.Player))
            {
                var player = info.Get<Player>();
                if (!player.isDieProcessing)
                {
                    warping = true;
                    effects.TryGenerate(transform.position);

                    info.transform.DOMove(to, teleportSpeed)
                        .SetEase(Ease.OutCubic)
                        .OnPlay(() => player.isTeleporting = true)
                        .OnComplete(() => player.isTeleporting = false);

                    warping = false;
                    // StartCoroutine(AfterDelay(info));
                }
            }
        }

        IEnumerator AfterDelay(Collider2D info)
        {
            yield return null;

            info.transform.SetPosition(to);
            warping = false;
        }
    }
}
