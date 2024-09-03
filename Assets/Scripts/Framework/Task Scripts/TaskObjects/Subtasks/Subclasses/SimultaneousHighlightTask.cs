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
        }

        public void DisableAllOutlines()
        {
            DisableAllOutline(); // Disable all outlines at once
        }
    }
}
