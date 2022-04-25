using System;
using Models;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class GameEventArgs<T> : EventArgs
    {
        public object Sender { get; set; }
        public T Value { get; set; }
    }
    
    public class GameEventManager: MonoBehaviour
    {
        public static GameEventManager Instance;
        
        public UnityEvent<GameEventArgs<PlayerStats>> playerStatsChanged = new();
        
        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        public void InvokePlayerStatsChanged(object sender, PlayerStats value)
        {
            playerStatsChanged.Invoke(new GameEventArgs<PlayerStats>
            {
                Sender = sender,
                Value = value
            });
        }
        
    }
}