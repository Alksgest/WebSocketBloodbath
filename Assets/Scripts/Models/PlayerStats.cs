using System;
using Models.Parameters;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class PlayerStats
    {
        [field: SerializeField] public Hp Hp { get; }
        [field: SerializeField] public Stamina Stamina { get; }

        public PlayerStats()
        {
            Hp = new Hp();
            Stamina = new Stamina();
        }
    }
}