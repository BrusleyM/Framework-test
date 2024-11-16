using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.Tasks;
using Framework.Audio;

namespace Framework.Sequential
{
    public class SequentialTask : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _audioSource;
        [SerializeField]
        private List<Task> _taskList;
        private Coroutine _currentCoroutine;
        private Task _currentTask;
        private AudioPlayer _audioPlayer;
        private bool _hasRun = false;

        public List<Task> TaskList => _taskList;
        public Task CurrentTask => _currentTask;
        public bool HasRun => _hasRun;

        private void OnEnable()
        {
            _audioPlayer = new AudioPlayer(_audioSource);
        }

        public void AddTask(Task task)
        {
            _taskList.Add(task);
        }

        public void Run()
        {
            if (_currentCoroutine == null && !_hasRun)
            {
                _hasRun = true;
                _currentCoroutine = StartCoroutine(RunTasks());
            }
        }

        private IEnumerator RunTasks()
        {
            foreach (var task in _taskList)
            {
                _currentTask = task;
                _currentTask.SetAudioPlayer(_audioPlayer);
                _currentTask.StartTask();

                yield return new WaitUntil(() => _currentTask.IsComplete);

                if (_currentTask is ActionTask actionTask)
                {
                    yield return new WaitUntil(() => !actionTask.IsFeedbackAudioPlaying());
                }

                _currentTask.OnCompleteTask?.Invoke();
            }

            StopExecution();
        }

        public void StopExecution()
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
                _currentCoroutine = null;
            }

            _taskList.Clear();
            _currentTask = null;
            _hasRun = false;
        }

        public void SkipCurrentTask()
        {
            _currentTask?.Skip();
            _currentTask?.TaskComplete();
        }
    }
}
