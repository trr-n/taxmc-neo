using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class Foot : MonoBehaviour
    {
        async void OnTriggerEnter2D(Collider2D info)
        {
            if (info.TryGet(out ICreature creature))
            {
                await creature.Die();
            }
        }
    }
}
