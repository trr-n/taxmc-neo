using Cysharp.Threading.Tasks;
using trrne.Bag;
using UnityEngine;
using System.Collections;

namespace trrne.Body
{
    public class Portal : Objectt
    {
        [SerializeField]
        Vector2 to;

        GameObject[] frames;
        float[] speeds;
        float myspeed;

        bool warping = false;

        protected override void Start()
        {
            base.Start();

            frames = new GameObject[transform.childCount];
            speeds = new float[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                frames[i] = transform.GetChilda(i);
                speeds[i] = Rand.Float(-30, 30);
            }
            myspeed = Rand.Float(-10, 10);
        }

        protected override void Behavior()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                frames[i].transform.Rotate(speeds[i] * Coordinate.z * Time.deltaTime);
            }
            transform.Rotate(myspeed * Coordinate.z * Time.deltaTime);
        }

        async void OnTriggerEnter2D(Collider2D info)
        {
            if (!warping && info.CompareBoth(Constant.Layers.Player, Constant.Tags.Player))
            {
                if (info.Try(out Player player) && player.isDieProcessing)
                {
                    return;
                }
                warping = true;

                effects.TryGenerate(transform.position);

                await UniTask.DelayFrame(App.fpsint / 10);
                info.transform.SetPosition(to);

                StartCoroutine(AfterDelay());
            }
        }

        IEnumerator AfterDelay()
        {
            yield return null;
            warping = false;
        }
    }
}
