using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Self.Game
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        int remainLives;

        int currentLives;

        public (int remain, int current) Lives => (remainLives, currentLives);

        public void Fluc(int amount)
        {
            if (currentLives <= 0)
                return;

            currentLives += amount;
            currentLives = Mathf.Clamp(currentLives, 0, remainLives);
        }

        public void Reset() => currentLives = remainLives;
    }
}
