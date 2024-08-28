using UnityEngine;
using System.Collections.Generic;

namespace Framework.Extensions
{
    public static class GameObjectExtension
    {
        private static Dictionary<GameObject, int> originalLayers = new Dictionary<GameObject, int>();
        public static void SetLayerRecursively(this GameObject obj, int newLayer)
        {
            if (obj == null)
            {
                return;
            }

            StoreOriginalLayer(obj);
            obj.layer = newLayer;

            foreach (Transform child in obj.transform)
            {
                if (child == null)
                {
                    continue;
                }
                child.gameObject.SetLayerRecursively(newLayer);
            }
        }
        public static void RevertLayerRecursively(this GameObject obj)
        {
            if (obj == null)
            {
                return;
            }

            if (originalLayers.TryGetValue(obj, out int originalLayer))
            {
                obj.layer = originalLayer;
            }
            else
            {
                obj.layer = 0;
            }

            foreach (Transform child in obj.transform)
            {
                if (child == null)
                {
                    continue;
                }
                child.gameObject.RevertLayerRecursively();
            }
        }
        private static void StoreOriginalLayer(GameObject obj)
        {
            if (!originalLayers.ContainsKey(obj))
            {
                originalLayers[obj] = obj.layer;
            }
        }
    }
}