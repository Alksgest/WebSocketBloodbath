using UnityEngine;

namespace Util
{
    public static class GameObjectExtension
    {
        public static bool HasParentInHierarchy(this GameObject go, Transform toFind)
        {
            var parent = go.transform.parent;
            while (true)
            {
                if (parent.gameObject == toFind.gameObject) return true; 
                
                if (parent.parent == null) return false;

                if (parent.parent != null)
                {
                    parent = parent.parent;
                }
            }
        }
    }
}