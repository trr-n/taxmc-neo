using UnityEngine;
using trrne.Teeth;
using Cysharp.Threading.Tasks;

namespace trrne.Body
{
    public class Venomin : Object
    {
        protected override void Start()
        {
            base.Start();
            animatable = false;
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
