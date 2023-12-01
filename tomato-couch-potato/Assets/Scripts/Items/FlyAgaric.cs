using UnityEngine;
using trrne.Box;
using Cysharp.Threading.Tasks;

namespace trrne.Core
{
    public class FlyAgaric : Object
    {
        protected override void Behavior() { }

        async void OnTriggerEnter2D(Collider2D _info)
        {
            if (_info.TryGetComponent(out Player player)
                && !player.IsDying)
            {
                effects.TryInstantiate(transform.position);
                await player.Die(Cause.Muscarine);
            }
        }
    }
}
