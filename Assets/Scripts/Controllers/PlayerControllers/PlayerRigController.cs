using System.Linq;
using UnityEngine;
using Util;

namespace Controllers.PlayerControllers
{
    public class PlayerRigController : MonoBehaviour
    {
        private Vector3 _defaultViewTargetPosition;

        [SerializeField] private float detectingDistance = 5f;
        [SerializeField] private Vector3 headVelocity = Vector3.zero;
        [SerializeField] private float headTurnTime = 0.3f;

        [SerializeField] private GameObject viewTarget;
        [SerializeField] private GameObject defaultViewTarget;
        
        private void Update()
        {
            PointTarget();
        }

        private void PointTarget()
        {
            var targets = GameObject.FindGameObjectsWithTag("ViewTarget");

            var objects = targets
                .Select(target =>
                {
                    var distance = Vector3.Distance(transform.position, target.transform.position);
                    return (distance, target);
                })
                .Where(el => el.distance <= detectingDistance && !el.target.HasParentInHierarchy(gameObject.transform))
                .ToList();

            if (objects.Any())
            {
                var target = 
                    objects.Aggregate((prev, next) => next.distance < prev.distance ? next : prev).target;

                viewTarget.transform.position = Vector3.SmoothDamp(
                    viewTarget.transform.position,
                    target.transform.position,
                    ref headVelocity, headTurnTime);
            }
            else
            {
                viewTarget.transform.position  = Vector3.SmoothDamp(
                    viewTarget.transform.position,
                    defaultViewTarget.transform.position,
                    ref headVelocity, headTurnTime);
            }
        }
    }
}