using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class Foot : MonoBehaviour
    {
        async void OnTriggerEnter2D(Collider2D info)
        {
            if (Gobject.TryGetComponent(info, out ICreature creature))
            {
                await creature.Die();
            }
        }
    }
}
