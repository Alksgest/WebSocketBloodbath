using System;
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
        
        public UnityEvent<GameEventArgs<int>> hpChanged = new();
        
        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        public void InvokeHpChanged(object sender, int value)
        {
            hpChanged.Invoke(new GameEventArgs<int>
            {
                Sender = sender,
                Value = value
            });
        }
    }
}