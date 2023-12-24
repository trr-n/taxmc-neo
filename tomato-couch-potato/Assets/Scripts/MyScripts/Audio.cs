using UnityEngine;

namespace trrne.Box
{
    public static class Audio
    {
        public static AudioClip TryPlayOneShot(
            this AudioSource source,
            AudioClip clip, float
            volume = 1f
        )
        {
            if (clip != null)
            {
                source.PlayOneShot(clip, volume);
                return clip;
            }
            return null;
        }

        public static AudioClip TryPlayOneShot(
            this AudioSource source,
            AudioClip[] clips,
            float volume = 1f
        )
        {
            if (clips.Length > 0)
            {
                AudioClip choice = clips.Choice();
                source.PlayOneShot(choice, volume);
                return choice;
            }
            return null;
        }
    }
}