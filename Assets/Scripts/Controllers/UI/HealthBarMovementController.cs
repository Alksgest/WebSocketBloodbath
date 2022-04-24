using UnityEngine;

namespace Controllers.UI
{
    public class HealthBarMovementController : MonoBehaviour
    {
        [SerializeField] private Transform cam;

        private void Start()
        {
            cam = Camera.main.transform;
        }

        private void LateUpdate()
        {
            transform.LookAt(transform.position + cam.forward);
        }
    }
}
