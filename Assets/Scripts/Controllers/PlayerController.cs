using Models;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        public string id;
        public bool isMainPlayer;
        public SceneManagerScript sceneManager;

        private Rigidbody _rigidbody;

        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float rotationSpeed = 200f;
        [SerializeField] private float bulletSpeed = 100f;
        
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletInitialPosition;

        public void Init(string id)
        {
            this.id = id;
        }
        
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
                HandleShooting();
            }
        }

        private void HandleShooting()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.R))
            {
                var shootPosition = bulletInitialPosition.position;
                
                var bullet = Instantiate(bulletPrefab, shootPosition, Quaternion.identity);
                var bulletRb = bullet.GetComponent<Rigidbody>();

                var velocity = transform.forward * bulletSpeed * Time.deltaTime;
                bulletRb.velocity = velocity;
                
                sceneManager.PlayerShoot(velocity, shootPosition);
            }
        }
        
        public void CreateBullet(Position shootPosition, Position shootVector)
        {
            var bullet = Instantiate(
                bulletPrefab, 
                new Vector3(shootPosition.X, shootPosition.Y, shootPosition.Z),
                Quaternion.identity);
            
            var bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.velocity = new Vector3(shootVector.X, shootVector.Y, shootVector.Z);
        }

        private void HandleMovement()
        {
           
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
                
                sceneManager.SyncPlayerState(gameObject);
            }
        }
    }
}
