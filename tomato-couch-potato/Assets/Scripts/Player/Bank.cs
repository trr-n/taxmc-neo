using UnityEngine;

namespace trrne.Core
{
    public class Bank : MonoBehaviour
    {
        int balance = 0;
        public int Balance => balance;

        public void Add(int amount) => balance += amount;
    }
}
