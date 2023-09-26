using trrne.Bag;
using UnityEngine;

namespace trrne.Body
{
    public class DeadZone : MonoBehaviour
    {
        // 場外
        async void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Fixed.Tags.Player))
            {
                await info.Get<Player>().Die();
                return;
            }

            switch (info.GetLayer())
            {
                case Fixed.Layers.Creature:
                    if (info.Try(out Enemy enemy))
                    {
                        await enemy.Die();
                    }
                    break;

                default: throw null;
            }
        }
    }
}