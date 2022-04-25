using System;
using Models.Parameters;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class PlayerStats
    {
        [SerializeField] private Hp hp;
        [SerializeField] private Stamina stamina;

        public Hp Hp => hp;
        public Stamina Stamina => stamina;

        public PlayerStats()
        {
            hp = new Hp();
            stamina = new Stamina();
        }
    }
}