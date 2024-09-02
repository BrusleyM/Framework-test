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
                    gameObject.GetComponent<CheckPoint>().OnChecked.AddListener(()=>IsComplete = true);
                    yield return new WaitUntil(() => gameObject.GetComponent<CheckPoint>().IsChecked);
                    IsComplete = true;
                    outline.DisableOutline();
                }
                else
                {
                    throw new InvalidOperationException("Outline component not found on GameObject.");
                }
            }
        }
    }
}
