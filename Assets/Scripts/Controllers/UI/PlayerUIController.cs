using System.Globalization;
using Managers;
using Models;
using Models.Player;
using TMPro;
using UnityEngine;

namespace Controllers.UI
{
    public class PlayerUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI hpValue;
        [SerializeField] private TextMeshProUGUI staminaValue;
        private void Start()
        {
            GameEventManager.Instance.playerStatsChanged.AddListener(OnPlayerStatsChanged);
            DontDestroyOnLoad(gameObject);
        }

        private void OnPlayerStatsChanged(GameEventArgs<PlayerStats> args)
        {
            hpValue.text = ((int)args.Value.Hp.Value).ToString();
            staminaValue.text = ((int)args.Value.Stamina.Value).ToString();
        }
    }
}