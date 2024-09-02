using System;
using UnityEngine;

namespace Framework.Tasks
{
    public class EnableTask : GameObjectTask
    {
        [SerializeField]
        private GameObject gameObjectToEnable;

        protected void EnableGameObject()
        {
            if (gameObjectToEnable != null)
            {
                gameObjectToEnable.SetActive(true);
            }
            else
            {
                throw new InvalidOperationException("No GameObject assigned to EnableTask.");
            }
        }
        public void DisableGameObject()
        {
            if (gameObjectToEnable != null)
            {
                gameObjectToEnable.SetActive(false);
            }
            else
            {
                throw new InvalidOperationException("No GameObject assigned to EnableTask.");
            }
            IsComplete = true;
        }
        
        public override void Execute()
        {
            EnableGameObject();
        }

    }
}
