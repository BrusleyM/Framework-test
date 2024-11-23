using System;
using Framework.CheckPoints;
using UnityEngine;

namespace Framework.Tasks
{
    public abstract class HighlightTask : GameObjectTask
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
            foreach (GameObject go in gameObjectsToHighlight)
            {
                var outline = go.GetComponent<Outline>();

                if (outline != null)
                {
                    outline.EnableOutline();
                }
                else
                {
                    throw new InvalidOperationException("Outline component not found on GameObject.");
                }
            }
            IsComplete = true;
        }

        public virtual void DisableAllOutline()
        {
            foreach (var gameObject in gameObjectsToHighlight)
            {
                DisableOutline(gameObject);
            }
            IsComplete = true;
        }

        public virtual void DisableOutline(GameObject gameObject)
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

        public override void Execute()
        {
            EnableOutline();
        }
    }
}
