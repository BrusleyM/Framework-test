using System.Collections;
using System.Collections.Generic;
using Framework.Audio;
using UnityEngine;

namespace Framework.Tasks
{
    public class AudioTask : Task
    {

        [SerializeField]
        private List<MyClip> _audioClips;

        public AudioTask(List<MyClip> audioClips)
        {
            _audioClips = audioClips ?? new List<MyClip>();
        }

        public override void StartTask()
        {
            if (_audioClips.Count > 0)
            {
                StartAudioSequence();
            }
            else
            {
                Debug.LogWarning("No audio clips found for this AudioTask.");
                TaskComplete();
            }
        }

        private void StartAudioSequence()
        {
            if (_audioPlayer != null)
            {
                StartCoroutine(PlayAudioSequence());
            }
            else
            {
                Debug.LogError("AudioPlayer is not set for this AudioTask.");
                TaskComplete();
            }
        }

        private IEnumerator PlayAudioSequence()
        {
            foreach (var clip in _audioClips)
            {
                if (clip != null)
                {
                    _audioPlayer.LoadAndPlay(clip.clip);
                    yield return new WaitWhile(() => _audioPlayer.IsPlaying());
                }
            }

            TaskComplete(); // Mark the task as complete once all clips have played
        }
    }
}

