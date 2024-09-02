using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Tasks
{
    public class EnableAndHighlightTask : GameObjectTask
    {
        [SerializeField]
        private GameObject gameObjectToEnable;
        [SerializeField]
        private HighlightTask highlightTask;

        public override void Execute()
        {
            gameObjectToEnable.SetActive(true);
            highlightTask.Execute();
        }
    }
}
