using UnityEngine;
using trrne.Core;
using trrne.Box;
using Cysharp.Threading.Tasks;

namespace trrne.Brain
{
    public class Clear : MonoBehaviour
    {
        [SerializeField]
        GameObject clearFx;

        [SerializeField]
        AudioClip clearSe;

        [SerializeField]
        [Header("0: down\n1: up")]
        Sprite[] flagSprites;

        [SerializeField]
        bool isReturnSelectScene = false;

        SpriteRenderer sr;
        AudioSource speaker;

        void Start()
        {
            speaker = GetComponent<AudioSource>();
            sr = GetComponent<SpriteRenderer>();
            sr.sprite = flagSprites[0];
        }

        async void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player _) && Gobject.TryGetWithTag(out TimeManager manager, Constant.Tags.MANAGER))
            {
                sr.sprite = flagSprites[1];
                speaker.TryPlayOneShot(clearSe);
                MainGameManager.Instance.WriteSaveData(Scenes.Active(), manager.CurrentTimeStr);
                MainGameManager.Instance.ClearStage();
                float longestParticleDuration = .5f;
                for (int i = 0; clearFx != null && i < clearFx.transform.childCount; ++i)
                {
                    if (clearFx.transform.GetChild(i).TryGetComponent(out ParticleSystem particle))
                    {
                        if (longestParticleDuration < particle.main.duration)
                        {
                            longestParticleDuration = particle.main.duration;
                        }
                    }
                }
                await UniTask.WaitForSeconds(longestParticleDuration);
                string next;
                if (isReturnSelectScene)
                {
                    next = Constant.Scenes.SELECT;
                }
                else
                {
                    var clearSceneIndex = int.Parse(Scenes.Active().Delete(Constant.Scenes.PREFIX));
                    var timeManager = Gobject.GetWithTag<TimeManager>(Constant.Tags.MANAGER);
                    if (clearSceneIndex >= MainGameManager.MAX - 1)
                    {
                        next = Constant.Scenes.CLEAR;
                        MainGameManager.Instance.Scores[^1] = timeManager.CurrentTimeStr;
                    }
                    else
                    {
                        next = Constant.Scenes.PREFIX + (clearSceneIndex + 1).ToString();
                        MainGameManager.Instance.Scores[0] = timeManager.CurrentTimeStr;
                    }
                }
                MainGameManager.Instance.SceneTransition(next);
            }
        }
    }
}
