using System;
using UnityEngine;

namespace Framework.Tasks
{
    public class HighlightTask : GameObjectTask
    {
        [SerializeField]
        protected GameObject[] gameObjectsToHighlight;

        protected virtual void InitializeOutline()
        {
            if (gameObjectsToHighlight == null || gameObjectsToHighlight.Length == 0)
            {
                throw new InvalidOperationException("No GameObjects provided for highlighting.");
            }
        }

        protected virtual void EnableOutline()
        {
            foreach (var gameObject in gameObjectsToHighlight)
            {
                var outline = gameObject.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.EnableOutline();
                }
                else
                {
                    throw new InvalidOperationException("Outline component not found on GameObject.");
                }
            }
        }

        public virtual void DisableOutline()
        {
            foreach (var gameObject in gameObjectsToHighlight)
            {
                var outline = gameObject.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.DisableOutline();
                }
                else
                {
                    throw new InvalidOperationException("Outline component not found on GameObject.");
                }
            }
            IsComplete = true;
        }

        public override void Execute()
        {
            EnableOutline();
        }
    }
}
