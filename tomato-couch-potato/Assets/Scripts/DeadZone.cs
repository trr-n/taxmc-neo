using UnityEngine;

namespace trrne.Core
{
    public class DeadZone : MonoBehaviour
    {
        async void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ICreature creature))
            {
                await creature.Die();
                return;
            }
            Destroy(other.gameObject);
        }
    }
}