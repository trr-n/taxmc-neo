using trrne.Bag;
using UnityEngine;

namespace trrne.Body
{
    public class Foot : MonoBehaviour
    {
        async void OnTriggerEnter2D(Collider2D info)
        {
            switch (info.GetLayer())
            {
                case Constant.Layers.Player:
                    if (info.TryGet(out Player player))
                    {
                        await player.Die();
                    }
                    break;

                case Constant.Layers.Creature:
                    if (info.TryGet(out Enemy enemy))
                    {
                        await enemy.Die();
                    }
                    break;
            }
        }
    }
}
