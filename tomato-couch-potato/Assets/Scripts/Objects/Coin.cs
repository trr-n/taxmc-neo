using Chickenen.Pancreas;
using UnityEngine;

namespace Chickenen.Heart
{
    public class Coin : MonoBehaviour
    {
        [SerializeField]
        int amount;

        void OnTriggerEnter2D(Collider2D info)
        {
            if (info.TryGet(out Bank bank))
            {
                bank.Fluc(amount);
                Destroy(gameObject);
            }
        }
    }
}
