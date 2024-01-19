using UnityEngine;

namespace trrne.Core
{
    public class Foot : MonoBehaviour
    {
        async void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
            {
                await player.Die(Cause.Hizakarakuzureotiru);
            }
            else if (other.TryGetComponent(out ICreature creature))
            {
                await creature.Die();
            }
        }
    }
}
