using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Framework.CheckPoints
{
    public class CheckPointsParent : MonoBehaviour
    {
        [SerializeField]
        private List<CheckPoint> _points;
        [SerializeField]
        private UnityEvent _onAllChecked;

        public List<CheckPoint> CheckPoints => _points;
        public UnityEvent OnAllChecked => _onAllChecked;

        private void Awake()
        {
            foreach (var point in _points)
            {
                point.Parent = this;
            }
        }

        public void CheckAllPoints()
        {
            if (AreAllPointsChecked())
            {
                _onAllChecked?.Invoke();
            }
        }

        public bool AreAllPointsChecked()
        {
            foreach (var point in _points)
            {
                if (!point.IsChecked)
                {
                    return false;
                }
            }
            return true;
        }

        public void ResetAllPoints()
        {
            foreach (var point in _points)
            {
                point.ResetPoint();
            }
        }
    }
}
