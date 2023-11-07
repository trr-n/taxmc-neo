using UnityEngine;
using trrne.Box;
using Cysharp.Threading.Tasks;

namespace trrne.Core
{
    public class ChiliPepper : Object
    {
        protected override void Behavior() { }

        async void OnTriggerEnter2D(Collider2D info)
        {
            if (info.TryGetComponent(out Player player) && !player.IsDieProcessing)
            {
                effects.TryGenerate(transform.position);
                await player.Die(CuzOfDeath.Venom);
            }
        }
    }
}
