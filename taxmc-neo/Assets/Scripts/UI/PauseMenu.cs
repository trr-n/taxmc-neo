using trrne.Bag;
using UnityEngine;

namespace trrne.Brain
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        GameObject panel;

        // IPlayer player;

        bool active;
        public bool isActive => active;

        void Start()
        {
            // player = Scenes.active switch
            // {
            //     Constant.Scenes.StageSelect => Gobject.GetWithTag<Arm.Player>(Constant.Tags.Player),
            //     _ => Gobject.GetWithTag<Body.Player>(Constant.Tags.Player)
            // };
        }

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
        }
    }
}
