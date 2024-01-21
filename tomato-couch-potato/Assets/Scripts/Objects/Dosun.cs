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
        new Rigidbody2D rigidbody;

        void Awake()
        {
            initPos = transform.position;
        }

        protected override void Start()
        {
            base.Start();

            sr.sprite = sprites[0];
            rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.gravityScale = 0;
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
                rigidbody.velocity += Vec.Make2(y: -Time.deltaTime * dossunPower * down);
            }
        }

        async void OnTriggerEnter2D(Collider2D other)
        {
            if (!isDossun || startDelayTimer.isRunning)
            {
                return;
            }
            print("hit for someone");

            if (other.TryGetComponent(out Player player))
            {
                print("player is tubusita");
                await player.Die(Cause.Hizakarakuzureotiru);
            }
            else if (other.TryGetComponent(out ICreature creature))
            {
                await creature.Die();
            }

            if (other.CompareLayer(Constant.Layers.JUMPABLE))
            {
                PlayOneShot(ses.Choice());
                isDossun = false;
                dossunPower = 0f;
                sr.sprite = sprites[0];

                // initPosに移動
                rigidbody.DOMove(initPos, up)
                // transform.DOMove(initPos, up)
                    .SetEase(Ease.OutCubic)
                    .OnStart(() => sr.sprite = sprites[0])
                    .OnComplete(async () =>
                    {
                        sr.sprite = sprites[1];
                        await UniTask.WaitForSeconds(interval); // interval秒経過後落下する
                        isDossun = true;
                    });
            }
        }
    }
}
