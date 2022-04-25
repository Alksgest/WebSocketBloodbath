using UnityEngine;
using UnityEngine.InputSystem;

namespace Util
{
    public static class UIHelper
    {
        public static Transform[] GetChildren(this Transform target)
        {
            if (!target)
            {
                throw new System.ArgumentNullException(nameof(target));
            }

            var children = new Transform[target.childCount];
            for (var i = 0; i < target.childCount; i++)
            {
                children[i] = target.GetChild(i);
            }

            return children;
        }

        public static object GetFieldValue(object obj, string fieldName)
        {
            var field = obj.GetType().GetField(fieldName);
            var value = field?.GetValue(obj);
            return value;
        }
        
        public static T GetObjectUnderClick<T>() where T : class
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            var rayPosition = new Vector2(mousePosition.x, mousePosition.y);
            var hit = Physics2D.Raycast(rayPosition, Vector2.zero);

            var value = hit.collider != null ? hit.collider.gameObject.GetComponent<T>() : null;
            
            return value;
        }
    }
}