using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class TomPay : MonoBehaviour
    {
        [SerializeField]
        AudioClip sound;

        public bool IsPaid { get; private set; }

        Bank bank;

        void Start()
        {
            bank = Gobject.GetWithTag<Bank>(Constant.Tags.Player);
        }

        void OnTriggerEnter2D(Collider2D info)
        {
            if (info.CompareTag(Constant.Tags.Player))
            {
                bank.Add(12);
                IsPaid = true;
            }
        }
    }
}
