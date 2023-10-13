using trrne.Bag;
using UnityEngine;

namespace trrne.Brain
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        GameObject panel;

        Body.IPlayer player;

        bool active = false;

        void Start()
        {
            panel.SetActive(active);

            player = Scenes.active == Constant.Scenes.StageSelect ?
                Gobject.GetWithTag<Body.Select.Player>(Constant.Tags.Player) :
                Gobject.GetWithTag<Body.Player>(Constant.Tags.Player);
        }

        public void PanelActivator()
        {
            if (Inputs.Down(Constant.Keys.Pause))
            {
                // pause
                if (!panel.activeSelf)
                {
                    Act(true);
                    // active = true;
                    // panel.SetActive(true);
                    // player.controllable = false;
                }

                // resume
                else
                {
                    Act(false);
                    active = false;
                    panel.SetActive(false);
                    player.controllable = true;
                }
            }
        }

        public void PanelActivator(bool active)
        {
            Act(active);
        }

        void Act(bool active)
        {
            this.active = active;
            panel.SetActive(active);
            player.controllable = !active;

        }
    }
}
