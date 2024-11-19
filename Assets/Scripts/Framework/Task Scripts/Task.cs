using Framework.Audio;
using UnityEngine;
using UnityEngine.Events;

namespace Framework.Tasks
{
    public abstract class Task : MonoBehaviour
    {
        [SerializeField]
        private string _taskName;
        protected AudioPlayer _audioPlayer;

        public string TaskName => _taskName;
        public bool IsDoing { get; protected set; }
        public bool IsComplete { get; protected set; }
        public bool IsSkip { get; protected set; }

        [SerializeField]
        private UnityEvent _onCompleteTask;
        public UnityEvent OnCompleteTask => _onCompleteTask;

        public void SetAudioPlayer(AudioPlayer player)
        {
            _audioPlayer = player;
        }

        public abstract void StartTask();

        public virtual void TaskComplete()
        {
            if (!IsComplete)
            {
                IsComplete = true;
                IsDoing = false;
                _onCompleteTask?.Invoke();
            }
        }

        public virtual void Skip()
        {
            IsSkip = true;
        }
    }
}
