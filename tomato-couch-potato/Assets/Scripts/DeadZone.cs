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
                case Config.Layers.Player:
                    info.TryAction<Player>(async player => await player.Die());
                    break;
                case Config.Layers.Creature:
                    info.TryAction<Creature>(async enemy => await enemy.Die());
                    break;
            }
        }
    }
}