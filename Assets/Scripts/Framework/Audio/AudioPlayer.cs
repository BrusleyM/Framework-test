using UnityEngine;

namespace Framework.Audio
{
    public class AudioPlayer
    {
        private AudioSource audioSource;

        public AudioPlayer(AudioSource audioSource)
        {
            this.audioSource = audioSource;
        }

        public void LoadAndPlay(AudioClip clip)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
        public bool IsPlaying()
        {
            return audioSource.isPlaying;
        }
        public bool IsPlaying(AudioClip clip)
        {
            return audioSource.isPlaying && audioSource.clip == clip;
        }

        public void Stop()
        {
            audioSource.Stop();
        }

    }
}
