using Managers;
using Models.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Controllers
{
    public sealed class PlayerStatsController : MonoBehaviour
    {
        public Player Player => player;
        
        [ShowInInspector, ReadOnly] private Player player;
        
        public void Init(Player p)
        {
            player = p;
            
            GameEventManager.Instance.InvokePlayerStatsChanged(this, player.PlayerStats);
        }

        public void ReduceStamina(float value)
        {
            var stamina = player.PlayerStats.Stamina;
            if(stamina.Value == stamina.MinValue) return;

            stamina.Value -= value;

            if (stamina.Value <= stamina.MinValue)
            {
                stamina.Value = 0;
            }
            
            GameEventManager.Instance.InvokePlayerStatsChanged(this, player.PlayerStats);
        }

        public void IncreaseStamina(float value)
        {
            var stamina = player.PlayerStats.Stamina;
            
            if(stamina.Value == stamina.MaxValue) return;

            stamina.Value += value;

            if (stamina.Value >= stamina.MaxValue)
            {
                stamina.Value = stamina.MaxValue;
            }
            
            GameEventManager.Instance.InvokePlayerStatsChanged(this, player.PlayerStats);
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
