using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class PlayerStats
    {
        [SerializeField] private int hp;
        [SerializeField] private int hits;

        public int Hp
        {
            get => hp;
            set => hp = value;
        }

        public int Hits
        {
            get => hits;
            set => hits = value;
        }
    }
}