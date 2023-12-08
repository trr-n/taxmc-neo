using System;
using trrne.Box;
using UnityEngine;

namespace trrne.Core
{
    public class DeadZone : MonoBehaviour
    {
        async void OnTriggerEnter2D(Collider2D info)
        {
            if (info.TryGetComponent(out ICreature creature))
            {
                await creature.Die();
            }
            else
            {
                try
                {
                    Destroy(info.gameObject);
                }
                catch { }
            }
        }
    }
}