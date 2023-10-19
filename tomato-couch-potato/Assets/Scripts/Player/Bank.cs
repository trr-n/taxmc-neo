using UnityEngine;

namespace Chickenen.Heart
{
    public class Bank : MonoBehaviour
    {
        int balance = 0;
        public int Balance => balance;

        public void Fluc(int amount)
        {
            balance -= amount;
        }
    }
}
