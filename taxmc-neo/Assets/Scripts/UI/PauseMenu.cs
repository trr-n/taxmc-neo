using trrne.Bag;
using UnityEngine;

namespace trrne.Brain
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        GameObject panel;

        bool active;
        public bool isActive => active;

        void Update()
        {
            active = gameObject.activeSelf;
            Activate();
        }

        void Activate()
        {
            if (Inputs.Down(Constant.Keys.Pause))
            {
                Act(!panel.activeSelf);
            }
        }

        public void Active()
        {
            Act(true);
        }

        public void Inactive()
        {
            Act(false);
        }

        void Act(bool active)
        {
            panel.SetActive(active);
            App.SetTimeScale(active ? 0 : 1);
        }
    }
}
