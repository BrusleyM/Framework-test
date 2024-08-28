using Framework.Sequential;
using UnityEngine;

namespace FrameworkTest
{
    public class GameManager : MonoBehaviour
    {
        public enum GameMode { Guided, LetMeTry }
        public GameMode CurrentMode { get; private set; }

        [SerializeField]
        private SequentialTask _currentSequentialTask;

        private void Start()
        {
            // Example: Start in Guided mode
            CurrentMode = GameMode.Guided;
            StartSequence();
        }

        public void SetGameMode(GameMode mode)
        {
            CurrentMode = mode;
        }

        public void StartSequence()
        {
            if (_currentSequentialTask != null)
            {
                if (CurrentMode == GameMode.Guided)
                {
                    _currentSequentialTask.Run();
                }
                else if (CurrentMode == GameMode.LetMeTry)
                {
                    // In "Let Me Try" mode, the tasks can be started manually or with hints.
                    // For now, you can start the sequence as in guided mode,
                    // but allow the player to interact with hints or skip tasks.
                    _currentSequentialTask.Run();
                }
            }
            else
            {
                Debug.LogError("No SequentialTask assigned to GameManager.");
            }
        }

        public void HintButtonPressed()
        {
            if (CurrentMode == GameMode.LetMeTry)
            {
                _currentSequentialTask?.CurrentTask?.Skip();
            }
        }
    }
}
