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
            if (info.CompareBoth(Constant.Layers.Player, Constant.Tags.Player))
            {
                effects.TryGenerate(transform.position);
                await info.Get<Player>().Die(CuzOfDeath.Venom);
            }
        }
    }
}
