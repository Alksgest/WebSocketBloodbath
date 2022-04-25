using Managers;
using Models;
using UnityEngine;

namespace Controllers
{
    public sealed class PlayerStatsController : MonoBehaviour
    {
        public Player Player => player;
        
        [SerializeField] private Player player;
        
        public void Init(Player p)
        {
            player = p;
            
            GameEventManager.Instance.InvokePlayerStatsChanged(this, player.PlayerStats);
        }

        public void ReduceStamina(float value)
        {
            if(player.PlayerStats.Stamina.Value == 0) return;

            player.PlayerStats.Stamina.Value -= value;

            if (player.PlayerStats.Stamina.Value < 0)
            {
                player.PlayerStats.Stamina.Value = 0;
            }
            
            GameEventManager.Instance.InvokePlayerStatsChanged(this, player.PlayerStats);
        }

        public void IncreaseStamina(float value)
        {
            
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            var res = collision.gameObject.TryGetComponent<BulletController>(out var bullet);
            
            if (!res) return;
            
            if (bullet.bullet.PlayerId == player.Id) return;

            player.PlayerStats.Hp.Value -= 1;
        }
        
        public void DestroyPlayer()
        {
            Destroy(gameObject);
        }
    }
}
