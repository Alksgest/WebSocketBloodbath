using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class Bullet
    {
        [field: SerializeField] public Guid Id { get; }
        [field: SerializeField] public Guid PlayerId { get; }
    }
}