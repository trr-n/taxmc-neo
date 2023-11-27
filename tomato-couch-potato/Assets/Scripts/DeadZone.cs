using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class DeadZone : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D info)
        {
            switch (info.GetLayer())
            {
                case Constant.Layers.Player:
                    info.TryAction<Player>(async player => await player.Die());
                    break;
                case Constant.Layers.Creature:
                    info.TryAction<Creature>(async enemy => await enemy.Die());
                    break;
            }
        }
    }
}