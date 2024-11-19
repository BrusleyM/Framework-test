using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Tasks
{
    public class TrackCheckPoint : GameObjectTask
    {
        [SerializeField]
        ActionTask _taskPoints;
        public override void Execute()
        {
            _taskPoints.CheckPointsParent.OnAllChecked.AddListener(() => IsComplete = true);
        }
    }
}
