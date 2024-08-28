using System;
using System.Collections;
using Framework.Audio;
using Framework.CheckPoints;
using Framework.Extensions;
using UnityEngine;

namespace Framework.Tasks
{
    public class ActionTask : Task
    {
        [SerializeField]
        private MyClip _instructionalAudio;
        [SerializeField]
        private MyClip _feedbackAudioClip;
        [SerializeField]
        private float _reminderInterval;
        [SerializeField]
        private CheckPointsParent _enableObjects;
        [SerializeField]
        private CheckPointsParent _outlineObjects;
        [SerializeField]
        private LayerMask _outlineLayer;

        private Coroutine _reminderCoroutine;

        public override void StartTask()
        {
            IsComplete = false;
            IsDoing = true;

            PlayInstructionalAudio();
            StartReminder();

            if (_enableObjects != null)
            {
                _enableObjects.OnAllChecked.AddListener(TaskComplete);
                EnableTaskObjectives();
            }

            if (_outlineObjects != null)
            {
                HighlightObjects();
            }
        }

        public override void TaskComplete()
        {
            StopReminder();
            PlayFeedbackAudio();
            IsComplete = true;
            ResetCheckpoints();
        }

        public override void Skip()
        {
            base.Skip();
            CompleteObjectives();
        }

        public bool IsFeedbackAudioPlaying()
        {
            return _audioPlayer?.IsPlaying() ?? false;
        }

        private void PlayInstructionalAudio()
        {
            _audioPlayer?.LoadAndPlay(_instructionalAudio?.clip);
        }

        private void PlayFeedbackAudio()
        {
            if (!IsSkip)
            {
                _audioPlayer?.LoadAndPlay(_feedbackAudioClip?.clip);
            }
        }

        private void StartReminder()
        {
            if (_reminderInterval > 0)
            {
                _reminderCoroutine = StartCoroutine(ReminderRoutine());
            }
        }

        private void StopReminder()
        {
            if (_reminderCoroutine != null)
            {
                StopCoroutine(_reminderCoroutine);
                _reminderCoroutine = null;
            }
        }

        private IEnumerator ReminderRoutine()
        {
            while (!IsComplete)
            {
                yield return new WaitForSeconds(_reminderInterval);

                if (!IsComplete && !IsDoing)
                {
                    PlayInstructionalAudio();
                }
            }
        }

        private void EnableTaskObjectives()
        {
            foreach (var checkpoint in _enableObjects.CheckPoints)
            {
                checkpoint.gameObject.SetActive(true);
            }
        }

        private void HighlightObjects()
        {
            if (_outlineLayer == 0)
            {
                throw new InvalidOperationException("LayerMask has not been set.");
            }
            else
            {
                foreach (var checkpoint in _outlineObjects.CheckPoints)
                {
                    checkpoint.gameObject.SetLayerRecursively(_outlineLayer);
                }
            }
        }

        private void CompleteObjectives()
        {
            foreach (var checkpoint in _enableObjects.CheckPoints)
            {
                checkpoint.MarkAsChecked();
            }
        }

        private void ResetCheckpoints()
        {
            _enableObjects?.ResetAllPoints();
            _outlineObjects?.ResetAllPoints();
        }
    }
}
