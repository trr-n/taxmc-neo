using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using trrne.Box;
using System.ComponentModel;

namespace trrne.Core
{
    public class Dosun : Object
    {
        [SerializeField]
        float interval = 1f, accelRatio = 5f, startDelay = 0f;

        [Tooltip("速度")]
        [SerializeField]
        float down = 7.5f, up = 3f;

        [SerializeField]
        float minY = 15;

        readonly Stopwatch startDelayTimer = new(true);

        bool isFalling = true;
        float dossunPower = 0f;
        double dossunPower2 = 0;
        const float POWER_MAX = 20.0f;
        const float VOLUME_RED_RATIO = 1.2f;

        Vector3 initPos;
        Transform player;
        new Rigidbody2D rigidbody;

        // readonly Stopwatch ctTimer = new();

        const int GREEN = 0, RED = 1;

        readonly Stopwatch loopTimer = new();
        // int loopCount = 0;
        float pre = 0f;
        // float myDelta => loopTimer.msf - pre;

        readonly Timer timer = new();

        void Awake()
        {
            initPos = transform.position;
        }

        protected override void Start()
        {
            base.Start();

            sr.sprite = sprites[GREEN];
            rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.gravityScale = 0;
            player = Gobject.GetWithTag<Transform>(Constant.Tags.PLAYER);

            Invoke(nameof(StopTimer), startDelay);

            timer.Restart();
        }

        void StopTimer()
        {
            startDelayTimer.Stop();
            sr.sprite = sprites[RED];
            loopTimer.Start();
        }

        protected override void Behavior()
        {
            pre = loopTimer.millisecondf;
            if (isFalling && !startDelayTimer.isRunning)
            {
                dossunPower = Mathf.Clamp(dossunPower += Time.fixedDeltaTime * accelRatio, 0f, POWER_MAX);
                // transform.Translate(y: -Time.fixedDeltaTime * dossunPower * down);
                transform.Translate(y: -(float)timer.Delta * dossunPower * down);
            }

            if (transform.position.y < minY && isFalling && !startDelayTimer.isRunning)
            {
                // minY以下にいる、貫通した可能性
                isFalling = false;
                dossunPower2 = dossunPower = 0;
                sr.sprite = sprites[GREEN];
                ReturnInitPos();
            }
            timer.Restart();
        }

        async void OnTriggerEnter2D(Collider2D other)
        {
            if (!isFalling || startDelayTimer.isRunning)
            {
                // print("isDossun is false");
                return;
            }
            // print("hit for someone");

            if (other.TryGetComponent(out Player player))
            {
                // print("player is tubusita");
                await player.Die(Cause.Hizakarakuzureotiru);
            }
            else if (other.TryGetComponent(out ICreature creature))
            {
                await creature.Die();
            }

            if (other.CompareLayer(Constant.Layers.JUMPABLE))
            {
                // ctTimer.Stop();
                // print(ctTimer.secondf);
                // ctTimer.Reset();
                speaker.PlayOneShot(ses.Choice());
                isFalling = false;
                dossunPower2 = dossunPower = 0f;
                sr.sprite = sprites[GREEN];

                ReturnInitPos();
            }
        }

        void ReturnInitPos()
        => rigidbody.DOMove(initPos, up)
            .SetEase(Ease.OutCubic)
            .OnStart(() => sr.sprite = sprites[0])
            .OnComplete(async () =>
            {
                sr.sprite = sprites[RED];
                await UniTask.WaitForSeconds(interval); // interval秒経過後落下する
                isFalling = true;
            });
    }
}
