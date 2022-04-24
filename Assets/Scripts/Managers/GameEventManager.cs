using UnityEngine;

namespace Managers
{
    public class GameEventManager: MonoBehaviour
    {
        public static GameEventManager Instance;
        
        // public UnityEvent<GameEventArgs<IVisitable>> shipLanded = new();
        
        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        // public void InvokeShipLanded(object sender, IVisitable target)
        // {
        //     shipLanded.Invoke(new GameEventArgs<IVisitable>
        //     {
        //         Sender = sender,
        //         Value = target
        //     });
        // }
    }
}