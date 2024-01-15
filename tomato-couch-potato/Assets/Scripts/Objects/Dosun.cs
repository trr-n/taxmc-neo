using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using trrne.Box;

namespace trrne.Core
{
    public class Dosun : Object
    {
        [SerializeField]
        float interval = 1f, accelRatio = 5f, startDelay = 0f;

        [Header("速度")]
        [SerializeField]
        float down = 7.5f, up = 3f;

        readonly Stopwatch startDelayTimer = new(true);

        bool isDossun = true;
        float dossunPower = 0f;
        const float POWER_MAX = 20.0f;
        const float VOLUME_RED_RATIO = 1.2f;

        Vector3 initPos;
        Transform player;

        void Awake()
        {
            initPos = transform.position;
        }

        protected override void Start()
        {
            base.Start();

            GetComponent<Rigidbody2D>().gravityScale = 0;
            player = Gobject.GetWithTag<Transform>(Constant.Tags.PLAYER);

            Invoke(nameof(StopTimer), startDelay);
        }

        void StopTimer() => startDelayTimer.Stop();

        protected override void Behavior()
        {
            if (isDossun && !startDelayTimer.isRunning)
            {
                dossunPower += Time.deltaTime * accelRatio;
                dossunPower = Mathf.Clamp(dossunPower, 0f, POWER_MAX);
                transform.Translate(Vec.MakeVec2(y: -Time.deltaTime * dossunPower * down));
            }
        }

        async void OnTriggerEnter2D(Collider2D other)
        {
            if (!isDossun || startDelayTimer.isRunning)
            {
                return;
            }

            if (other.TryGetComponent(out ICreature creature))
            {
                await creature.Die();
            }
            if (other.CompareLayer(Constant.Layers.JUMPABLE))
            {
                isDossun = false;
                PlayOneShot(ses.Choice());
                dossunPower = 0f;

                // initPosに移動
                transform.DOMove(initPos, up)
                    .SetEase(Ease.OutCubic)
                    .OnComplete(async () =>
                    {
                        await UniTask.WaitForSeconds(interval); // interval秒経過後落下する
                        isDossun = true;
                    });
            }
        }
    }
}
