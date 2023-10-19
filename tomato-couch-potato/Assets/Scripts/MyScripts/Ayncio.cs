using UnityEngine;

namespace Chickenen.Pancreas
{
    public sealed class Asyncio
    {
        public static WaitForSeconds Wait(float seconds)
        {
            return new WaitForSeconds(seconds);
        }

        public static WaitForSeconds Wait()
        {
            return Wait(1f);
        }
    }
}
