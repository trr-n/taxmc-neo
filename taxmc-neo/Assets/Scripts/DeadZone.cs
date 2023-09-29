using trrne.Bag;
using UnityEngine;

namespace trrne.Body
{
    public class DeadZone : MonoBehaviour
    {
        // 場外
        void OnTriggerEnter2D(Collider2D info)
        {
            switch (info.GetLayer())
            {
                case Constant.Layers.Player:
                    info.TryAction<Player>(async player => await player.Die());
                    break;

                case Constant.Layers.Creature:
                    info.TryAction<Enemy>(async enemy => await enemy.Die());
                    break;

                default: throw null;
            }
        }
    }
}