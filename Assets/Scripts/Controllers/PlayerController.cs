using System;
using Managers;
using Models;
using UnityEngine;

namespace Controllers
{
    public class PlayerController : PlayerControllerBase
    {
        public override void Init(Player p)
        {
            base.Init(p);
            
            GameEventManager.Instance.InvokeHpChanged(this, player.PlayerStats.Hp);
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);

            GameEventManager.Instance.InvokeHpChanged(this, player.PlayerStats.Hp);
        }

        private void Update()
        {
            HandleMovement();
            HandleShooting();
        }

        private void HandleShooting()
        {
            if (!Input.GetMouseButtonDown(0) && !Input.GetKeyDown(KeyCode.R)) return;
            
            var shootPosition = bulletInitialPosition.position;

            var bullet = InstantiateBullet(
                shootPosition, 
                player, 
                Guid.NewGuid().ToString());

            var bulletRb = bullet.GetComponent<Rigidbody>();

            var velocity = transform.forward * (bulletSpeed * Time.deltaTime);
            bulletRb.velocity = velocity;

            GameManager.Instance.PlayerShoot(velocity, shootPosition);
        }
        
        private void HandleMovement()
        {
            if (Input.anyKey)
            {
                // left
                if (Input.GetKey(KeyCode.A))
                {
                    var newPosition = Rigidbody.position +
                                      transform.TransformDirection(-moveSpeed * Time.deltaTime, 0, 0);
                    Rigidbody.MovePosition(newPosition);
                }

                // right
                if (Input.GetKey(KeyCode.D))
                {
                    var newPosition = Rigidbody.position +
                                      transform.TransformDirection(moveSpeed * Time.deltaTime, 0, 0);
                    Rigidbody.MovePosition(newPosition);
                }

                // up
                if (Input.GetKey(KeyCode.W))
                {
                    var newPosition = Rigidbody.position +
                                      transform.TransformDirection(0, 0, moveSpeed * Time.deltaTime);
                    Rigidbody.MovePosition(newPosition);
                }

                // down
                if (Input.GetKey(KeyCode.S))
                {
                    var newPosition = Rigidbody.position +
                                      transform.TransformDirection(0, 0, -moveSpeed * Time.deltaTime);
                    Rigidbody.MovePosition(newPosition);
                }

                if (Input.GetKey(KeyCode.Space))
                {
                    var newPosition = Rigidbody.position + transform.TransformDirection(0, Time.deltaTime * 10, 0);
                    Rigidbody.MovePosition(newPosition);
                }

                if (Input.GetKey(KeyCode.Q))
                {
                    transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
                }

                if (Input.GetKey(KeyCode.E))
                {
                    transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
                }

                GameManager.Instance.SyncPlayerState(gameObject);
            }
        }
    }
}
