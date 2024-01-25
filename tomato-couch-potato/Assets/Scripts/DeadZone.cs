#if !UNITY_EDITOR
using trrne.Box;
#endif
using UnityEngine;

namespace trrne.Core
{
    public class DeadZone : MonoBehaviour
    {
#if !UNITY_EDITOR
        void Start()
        {
            GetComponent<SpriteRenderer>().SetAlpha(0);
        }
#endif

        async void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.root.name == "Grid")
            {
                return;
            }

            if (other.TryGetComponent(out ICreature creature))
            {
                await creature.Die();
                return;
            }
            Destroy(other.gameObject);
        }
    }
}