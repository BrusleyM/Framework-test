using System;
using System.Collections;
using Framework.Audio;
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
        private GameObjectTask[] _subTasks;

        private Coroutine _reminderCoroutine;
        private Coroutine _subTaskCoroutine;

        public override void StartTask()
        {
            IsComplete = false;
            IsDoing = true;

            PlayInstructionalAudio();
            StartReminder();
            _subTaskCoroutine = StartCoroutine(RunSubTasks());
        }
        private IEnumerator RunSubTasks()
        {
            foreach (var task in _subTasks)
            {
                task.Execute();
                yield return new WaitUntil(() => task.IsComplete);
            }

        }
        public override void TaskComplete()
        {
            StopReminder();
            PlayFeedbackAudio();
            IsComplete = true;
            //ResetCheckpoints();
        }

        public override void Skip()
        {
            StopReminder();
            if (_audioPlayer?.IsPlaying() == true)
            {
                _audioPlayer.Stop();
            }
            CompleteObjectives();
            IsComplete = true;
            IsDoing = false;
            if (_feedbackAudioClip != null)
            {
                PlayFeedbackAudio();
            }
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
        private void StopSubtask()
        {
            if (_subTaskCoroutine != null)
            {
                StopCoroutine(_subTaskCoroutine);
                _subTaskCoroutine = null;
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

        //private void EnableTaskObjectives()
        //{
        //    foreach (var checkpoint in _enableObjects.CheckPoints)
        //    {
        //        checkpoint.gameObject.SetActive(true);
        //    }
        //}

        //private void HighlightObjects()
        //{
        //    foreach (var checkpoint in _outlineObjects.CheckPoints)
        //    {
        //        var outline = checkpoint.gameObject.GetComponent<Outline>();
        //        if (outline != null)
        //        {
        //            outline.EnableOutline();
        //        }
        //        else
        //        {
        //            throw new InvalidOperationException("LayerMask has not been set.");
        //        }

        //    }

        //}

        private void CompleteObjectives()
        {
            foreach (var task in _subTasks)
            {
                if (task is EnableTask enableTask)
                {
                    enableTask.DisableGameObject(); // Disables the enabled GameObject
                }

                if (task is HighlightTask highlightTask)
                {
                    highlightTask.DisableOutline(); // Disables the outline effect on the GameObjects
                }

                // If there's any common completion logic, you can also invoke a method from the base class
                //task.MarkAsComplete(); // Assuming you have a method like this in GameObjectTask
            }
        }
        private void OnDisable()
        {
            StopReminder();
            StopSubtask();
            //EnableTaskObjectives();
            //HighlightObjects();
        }
        //private void ResetCheckpoints()
        //{
        //    _enableObjects?.ResetAllPoints();
        //    _outlineObjects?.ResetAllPoints();
        //}
    }
}
