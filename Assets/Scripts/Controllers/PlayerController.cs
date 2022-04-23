using UnityEngine;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public bool isMainPlayer;
        public SceneManagerScript sceneManager;

        private Rigidbody _rigidbody;

        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float rotationSpeed = 200f;
        [SerializeField] private float bulletSpeed = 100f;


        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletInitialPosition;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            if (!isMainPlayer)
            {

            }
        }

        private void Update()
        {
            if (isMainPlayer)
            {
                HandleMovement();
            }
        }

        private void HandleMovement()
        {
            var targetPos = transform.position;
            if (Input.anyKey)
            {
                // left
                if (Input.GetKey(KeyCode.A))
                {
                    var newPosition = _rigidbody.position + transform.TransformDirection(-moveSpeed * Time.deltaTime, 0, 0);
                    _rigidbody.MovePosition(newPosition);
                    // transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
                }

                // right
                if (Input.GetKey(KeyCode.D))
                {
                    var newPosition = _rigidbody.position + transform.TransformDirection(moveSpeed * Time.deltaTime, 0, 0);
                    _rigidbody.MovePosition(newPosition);
                    // transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
                }

                // up
                if (Input.GetKey(KeyCode.W))
                {
                    var newPosition = _rigidbody.position + transform.TransformDirection(0, 0, moveSpeed * Time.deltaTime);
                    _rigidbody.MovePosition(newPosition);
                    // transform.Translate(0, 0, moveSpeed * Time.deltaTime);
                }

                // down
                if (Input.GetKey(KeyCode.S))
                {
                    var newPosition = _rigidbody.position + transform.TransformDirection(0, 0, -moveSpeed * Time.deltaTime);
                    _rigidbody.MovePosition(newPosition);
                    // transform.Translate(0, 0, -moveSpeed * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.Space))
                {
                    var newPosition = _rigidbody.position + transform.TransformDirection(0, Time.deltaTime * 10, 0);
                    _rigidbody.MovePosition(newPosition);
                    // transform.Translate(0, 0, -moveSpeed * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.Q))
                {
                    transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
                }

                if (Input.GetKey(KeyCode.E))
                {
                    transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
                }

                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.R))
                {
                    var bullet = Instantiate(bulletPrefab, bulletInitialPosition.position, Quaternion.identity);
                    var bulletRb = bullet.GetComponent<Rigidbody>();

                    bulletRb.velocity = transform.forward * bulletSpeed * Time.deltaTime;
                }

                sceneManager.SyncPlayerState(gameObject);
            }
        }
    }
}
