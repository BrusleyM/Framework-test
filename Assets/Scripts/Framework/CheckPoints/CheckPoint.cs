using UnityEngine;
using UnityEngine.Events;

namespace Framework.CheckPoints
{
    public class CheckPoint : MonoBehaviour
    {
        [SerializeField]
        private bool _isChecked = false;
        [SerializeField]
        private bool _disableWhenComplete = false;

        public bool IsChecked => _isChecked;

        [SerializeField]
        private UnityEvent _onChecked;
        public UnityEvent OnChecked => _onChecked;

        [HideInInspector]
        public CheckPointsParent Parent;
        public void MarkAsChecked()
        {
            if (!_isChecked)
            {
                _isChecked = true;
                _onChecked?.Invoke();
                gameObject.SetActive(!_disableWhenComplete);
                Parent?.CheckAllPoints();
            }
        }

        public void ResetPoint()
        {
            _isChecked = false;
        }
        public void EnableCheckPoint()
        {
            gameObject.SetActive(true);
        }
    }
}
