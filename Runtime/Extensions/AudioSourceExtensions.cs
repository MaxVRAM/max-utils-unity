using UnityEngine;

namespace MaxVram.Extensions
{
    public static class AudioSourceExtensions
    {
        /// <summary>
        /// Returns the duration of the clip, taking into account the pitch of the AudioSource.
        /// </summary>
        public static float PitchedDuration(this AudioSource source)
        {
            return source.clip.length / Mathf.Abs(source.pitch);
        }

        /// <summary>
        /// If the AudioSource is not currently playing, this starts playback at a random position of the active clip.
        /// Use this on looped samples where more than one instance of the same clip are likely to play from multiple AudioSource components.
        /// This helps to prevent phasing issues caused by simultaneous (or near simultaneous) playback of identical content.
        /// </summary>
        public static void PlayFromRandomOffset(this AudioSource source)
        {
            if (source.isPlaying || source.clip == null)
                return;

            int startSample = Random.Range(0, source.clip.samples - 1);
            source.timeSamples = startSample;
            source.Play();
        }
    }
}
