using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using trrne.Box;
using trrne.Brain;

namespace trrne.Core
{
    public class Dosun : Object
    {
        [SerializeField]
        float interval = 1f, accelRatio = 5f, startDelay = 0f;

        [Header("速度")]
        [SerializeField]
        float down = 7.5f, up = 3f;

        readonly Stopwatch startDelaySW = new(true);

        bool isDossun = true;
        float dossunPower = 0f;
        (float MIN, float MAX) DOSSUN_POWER => (0f, 20f);
        const float DOSSUN_VOLUME_REDUCTION_RATIO = 1.2f;

        Rigidbody2D rb;
        Vector3 initPos;

        Transform player;

        protected override void Start()
        {
            base.Start();

            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;

            initPos = transform.position;

            player = Gobject.GetWithTag<Transform>(Config.Tags.PLAYER);

            Invoke(nameof(StopStartDelaySW), startDelay);
        }

        void StopStartDelaySW() => startDelaySW.Stop();

        protected override void Behavior()
        {
            if (!isDossun || startDelaySW.IsRunning())
            {
                return;
            }
            dossunPower += Time.deltaTime * accelRatio;
            dossunPower = Mathf.Clamp(dossunPower, DOSSUN_POWER.MIN, DOSSUN_POWER.MAX);
            transform.Translate(Time.deltaTime * dossunPower * down * -Vec.Y);
        }

        async void OnTriggerEnter2D(Collider2D other)
        {
            if (!isDossun || startDelaySW.IsRunning())
            {
                return;
            }

            if (other.CompareLayer(Config.Layers.JUMPABLE))
            {
                float distance = (transform.position - player.position).magnitude;
                Recorder.Instance.PlayOneShot(ses.Choice(), 1 / (distance / DOSSUN_VOLUME_REDUCTION_RATIO));

                dossunPower = 0f;
                isDossun = false;

                // initPosに移動
                transform.DOMove(initPos, up)
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
