using Chickenen.Pancreas;
using UnityEngine;

namespace Chickenen.Heart
{
    public class Foot : MonoBehaviour
    {
        async void OnTriggerEnter2D(Collider2D info)
        {
            if (info.TryGet(out IMurderable murder))
            {
                await murder.Die();
            }
        }
    }
}
