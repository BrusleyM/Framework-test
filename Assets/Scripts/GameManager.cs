using Framework.Sequential;
using UnityEngine;

namespace FrameworkTest
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance=> _instance;
        public enum GameMode { Guided, LetMeTry }
        [SerializeField]
        private GameMode _currentMode = GameMode.Guided;
        public GameMode CurrentMode => _currentMode;

        [SerializeField]
        private SequentialTask _currentSequentialTask;
        public SequentialTask CurrentSequentialTask => _currentSequentialTask;
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this);
            }
            else
            {
                _instance = this;
            }
        }

        private void Start()
        {
            // Example: Start in Guided mode
            _currentMode = GameMode.Guided;
            try
            {
                StartSequence();
            }
            catch (System.Exception e) { 
            Debug.LogException(e);
            }
        }

        public void SetGameMode(GameMode mode)
        {
            _currentMode = mode;
        }

        public void StartSequence()
        {
            if (_currentSequentialTask != null)
            {
                if (_currentMode == GameMode.Guided)
                {
                    _currentSequentialTask.Run();
                }
                else if (_currentMode == GameMode.LetMeTry)
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
            if (_currentMode == GameMode.LetMeTry)
            {
                _currentSequentialTask?.CurrentTask?.Skip();
            }
        }
    }
}
