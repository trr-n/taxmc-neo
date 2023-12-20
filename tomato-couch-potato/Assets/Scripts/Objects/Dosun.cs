using Cysharp.Threading.Tasks;
using DG.Tweening;
using trrne.Box;
using trrne.Brain;
using UnityEngine;

namespace trrne.Core
{
    public class Dosun : Object
    {
        [SerializeField]
        float interval = 1f, accelRatio = 5f, startDelay = 0f;

        readonly Stopwatch startDelaySW = new(true);

        bool isDossun = true;
        const float DOWN = 7.5f, UP = 3f;
        float power = 0f;

        Rigidbody2D rb;
        Vector3 initPos;

        protected override void Start()
        {
            base.Start();

            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;

            initPos = transform.position;

            Invoke(nameof(StopStartDelaySW), startDelay);
        }

        void StopStartDelaySW() => startDelaySW.Stop();

        protected override void Behavior()
        {
            if (!isDossun || startDelaySW.IsRunning())
            {
                return;
            }
            power += Time.deltaTime * accelRatio;
            transform.Translate(Time.deltaTime * power * DOWN * -Vec.Y);
        }

        async void OnTriggerEnter2D(Collider2D other)
        {
            if (!isDossun || startDelaySW.IsRunning())
            {
                return;
            }

            if (other.CompareLayer(Config.Layers.JUMPABLE))
            {
                Recorder.Instance.PlayOneShot(ses.Choice());

                power = 0f;
                isDossun = false;

                // initPosに移動
                transform.DOMove(initPos, UP)
                    .SetEase(Ease.OutCubic)
                    .OnComplete(async () =>
                    {
                        // interval秒経過後落下する
                        await UniTask.WaitForSeconds(interval);
                        isDossun = true;
                    });
            }
            else if (other.TryGetComponent(out ICreature creature))
            {
                await creature.Die();
            }
        }
    }
}
