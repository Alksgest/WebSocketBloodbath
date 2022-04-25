using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class Bullet
    {
        [field: SerializeField] public string Id { get; }
        [field: SerializeField] public string PlayerId { get; }
    }
}