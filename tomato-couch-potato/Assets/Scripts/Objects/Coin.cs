using UnityEngine;

namespace trrne.Core
{
    public class Coin : MonoBehaviour
    {
        [SerializeField]
        int amount;

        void OnTriggerEnter2D(Collider2D info)
        {
            if (info.TryGetComponent(out Bank bank))
            {
                bank.Add(amount);
                Destroy(gameObject);
            }
        }
    }
}
