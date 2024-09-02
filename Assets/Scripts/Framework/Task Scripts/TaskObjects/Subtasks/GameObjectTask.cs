using UnityEngine;

namespace Framework.Tasks
{
    public abstract class GameObjectTask : MonoBehaviour
    {
        public bool IsComplete { get; protected set; }
        public abstract void Execute();
    }
}
