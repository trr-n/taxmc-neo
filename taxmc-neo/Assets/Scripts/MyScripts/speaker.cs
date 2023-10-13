using UnityEngine;

namespace trrne.Bag
{
    public static class Speaker
    {
        public static void RandomPlayOneShot(this AudioSource speaker, AudioClip[] clips)
        {
            speaker.PlayOneShot(clips.Choice());
        }
    }
}