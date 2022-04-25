using Models;
using UnityEngine;
using WebSocketMessages;

namespace Controllers
{
    public class PlayerControllerBase : MonoBehaviour
    {
        [SerializeField] protected Player player;
        
        [SerializeField] protected float rotationSpeed = 200f;

        [SerializeField] protected Rigidbody rb;

        [SerializeField] protected Animator animator;
        
        protected static readonly int Move = Animator.StringToHash("Move");
        protected static readonly int Jump = Animator.StringToHash("Jump");
        protected static readonly int Attack = Animator.StringToHash("Attack");
        
        public void Init(Player p)
        {
            p.PlayerStats ??= new PlayerStats
            {
                Hp = 100
            };
            
            player = p;
        }
        
        
        protected  virtual void OnCollisionEnter(Collision collision)
        {
            var res = collision.gameObject.TryGetComponent<BulletController>(out var bullet);
            
            if (!res) return;
            
            if (bullet.bullet.PlayerId == player.Id) return;

            player.PlayerStats.Hp -= 1;
        }
        
        public virtual void DestroyPlayer()
        {
            Destroy(gameObject);
        }
        
        // public void CreateBullet(ServerMessagePlayerShoot shootMessage)
        // {
        //     var shootPosition = shootMessage.ShootPosition;
        //     var shootVector = shootMessage.ShootVector;
        //
        //     var bullet = InstantiateBullet(
        //         shootPosition,
        //         shootMessage.Player, 
        //         shootMessage.BulletId);
        //
        //     var bulletRb = bullet.GetComponent<Rigidbody>();
        //     bulletRb.velocity = new Vector3(shootVector.X, shootVector.Y, shootVector.Z);
        // }
        //
        // protected GameObject InstantiateBullet(
        //     Position shootPosition,
        //     Player shootingPlayer,
        //     string bulletId)
        // {
        //     var bullet = Instantiate(
        //         bulletPrefab,
        //         new Vector3(shootPosition.X, shootPosition.Y, shootPosition.Z),
        //         Quaternion.identity);
        //
        //     var bulletController = bullet.GetComponent<BulletController>();
        //     bulletController.Init(new Bullet
        //     {
        //         Id = bulletId,
        //         PlayerId = shootingPlayer.Id
        //     });
        //     return bullet;
        // }
    }
}