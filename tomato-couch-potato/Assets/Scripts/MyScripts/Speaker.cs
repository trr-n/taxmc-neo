using UnityEngine;

namespace Chickenen.Pancreas
{
    public static class Speaker
    {
        public static void TryPlay(this AudioSource source, AudioClip clip)
        {
            if (clip != null)
            {
                source.PlayOneShot(clip);
            }
        }
    }
}