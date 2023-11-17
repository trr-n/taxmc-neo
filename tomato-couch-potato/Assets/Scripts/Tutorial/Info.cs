using System.Collections;
using Cysharp.Threading.Tasks;
using trrne.Box;
using UnityEngine;
using UnityEngine.UI;

namespace trrne.Core
{
    public abstract class Info : MonoBehaviour
    {
        [SerializeField]
        protected Sprite infoSprite;

        protected bool isDone = false;

        protected GameObject tpanel;
        protected CanvasGroup group;
        protected Image image;

        protected virtual void Start()
        {
            tpanel = GameObject.Find("TutorialPanels");
            group = tpanel.GetComponent<CanvasGroup>();
            image = tpanel.GetComponentInChildren<Image>();
        }

        protected async virtual UniTask StartInfo()
        {
            var prescale = Time.timeScale;
            Time.timeScale = 0;
            await UniTask.WaitUntil(() => Inputs.Down(Constant.Keys.Button));
            Time.timeScale = prescale;
        }

        protected virtual IEnumerator e()
        {
            var prescale = Time.timeScale;
            Time.timeScale = 0;
            yield return new WaitUntil(() => Inputs.Down(Constant.Keys.Button));
            Time.timeScale = prescale;
        }

        protected abstract void OnTriggerEnter2D(Collider2D info);
    }
}