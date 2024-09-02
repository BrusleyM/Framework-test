using System;
using Framework.CheckPoints;
using UnityEngine;

namespace Framework.Tasks
{
    public class SimultaneousHighlightTask : HighlightTask
    {
        protected override void EnableOutline()
        {
            base.EnableOutline(); // Reuse the base implementation for enabling all outlines
            gameObject.GetComponent<CheckPoint>().OnChecked.AddListener(() => IsComplete = true);
        }

        public void DisableAllOutlines()
        {
            DisableOutline(); // Disable all outlines at once
        }
    }
}
