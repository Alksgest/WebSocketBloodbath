using System;
using UnityEngine;

namespace Models.Parameters
{
    [Serializable]
    public class ParameterBase : IParameter
    {
        [field: SerializeField] public float Value { get; set; }
        [field: SerializeField] public float MaxValue { get; set; }
        [field: SerializeField] public float MinValue { get; set; }
        [field: SerializeField] public float ConsumptionRate { get; set; }
        [field: SerializeField] public float RecoveryRate { get; set; }
    }
}