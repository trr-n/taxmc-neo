using Cysharp.Threading.Tasks;
using UnityEngine;

namespace trrne.Box
{
    public sealed class Asyncio
    {
        public static WaitForSeconds Wait(float seconds) => new(seconds);
        public static WaitForSeconds Wait() => Wait(1f);
        public static UniTask None() => UniTask.WaitForSeconds(0);
    }
}
