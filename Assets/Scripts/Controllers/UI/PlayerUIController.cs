using Managers;
using TMPro;
using UnityEngine;

namespace Controllers.UI
{
    public class PlayerUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI hpValue;
        private void Start()
        {
            GameEventManager.Instance.hpChanged.AddListener(OnHpChanged);
            DontDestroyOnLoad(gameObject);
        }

        private void OnHpChanged(GameEventArgs<int> args)
        {
            hpValue.text = args.Value.ToString();
        }
    }
}