using UnityEngine;
using trrne.Pancreas;
using Cysharp.Threading.Tasks;

namespace trrne.Heart
{
    public class Venomin : Object
    {
        protected override void Start()
        {
            base.Start();
            Animatable = false;
        }

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
