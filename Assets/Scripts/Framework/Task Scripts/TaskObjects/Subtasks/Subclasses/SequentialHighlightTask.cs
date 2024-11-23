using System;
using System.Collections;
using Framework.CheckPoints;
using UnityEngine;

namespace Framework.Tasks
{
    public class SequentialHighlightTask : HighlightTask
    {
        protected override void EnableOutline()
        {
            StartCoroutine(SequentialHighlightRoutine());
        }

        private IEnumerator SequentialHighlightRoutine()
        {
            foreach (var gameObject in gameObjectsToHighlight)
            {
                var outline = gameObject.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.EnableOutline();
                    yield return new WaitUntil(() => !outline.IsOutLined);
                    IsComplete = true;
                }
                else
                {
                    throw new InvalidOperationException("Outline component not found on GameObject.");
                }
            }
        }
    }
}
