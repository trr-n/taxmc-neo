using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class EntitySpawner : MonoBehaviour
    {
        [SerializeField]
        GameObject entity;

        [SerializeField]
        float span = 1;

        readonly Stopwatch timer = new(true);

        void Update()
        {
            if (timer.sf >= span)
            {
                SpawnEntity();
                timer.Restart();
            }
        }

        void SpawnEntity()
        {
            try
            {
                entity.Instantiate(transform.position);
            }
            catch (UnassignedReferenceException e)
            {
                throw new KarappoyankeException(e.Message);
            }
        }
    }
}
