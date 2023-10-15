using UnityEngine;

namespace trrne.WisdomTeeth
{
    public static class Speaker
    {
        public static void RandomPlayOneShot(this AudioSource speaker, AudioClip[] clips)
        {
            speaker.PlayOneShot(clips.Choice());
        }
    }
}
