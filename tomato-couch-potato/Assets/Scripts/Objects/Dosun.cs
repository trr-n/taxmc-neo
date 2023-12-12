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
        float interval = 1f;

        [SerializeField]
        float accelRatio = 5f;

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
        }

        protected override void Behavior()
        {
            if (isDossun)
            {
                power += Time.deltaTime * accelRatio;
                transform.Translate(Time.deltaTime * power * DOWN * -Vec.Y);
            }
        }

        async void OnTriggerEnter2D(Collider2D other)
        {
            // 落下中
            if (isDossun)
            {
                // 地面にあたったら
                if (other.CompareLayer(Config.Layers.Jumpable))
                {
                    Recorder.Instance.PlayOneShot(ses.Choice());

                    power = 0f;
                    isDossun = false;
                    // 初期座標に移動
                    transform.DOMove(initPos, UP)
                        .SetEase(Ease.OutCubic)
                        .OnComplete(async () =>
                        {
                            // interval秒経過後落下させる
                            await UniTask.WaitForSeconds(interval);
                            isDossun = true;
                        });
                }

                if (other.TryGetComponent(out ICreature creature))
                {
                    await creature.Die();
                }
            }
        }
    }
}
