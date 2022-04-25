using System;
using UnityEngine;

namespace Models.Parameters
{
    [Serializable]
    public class ParameterBase : IParameter
    {
        [SerializeField] protected float value;
        [SerializeField] protected float maxValue = 100;
        [SerializeField] protected float minValue = 0;
        [SerializeField] protected float consumptionRate = 10f;
        [SerializeField] protected float recoveryRate = 10f;

        public float Value
        {
            get => value;
            set => this.value = value;
        }

        public float MaxValue
        {
            get => maxValue;
            set => maxValue = value;
        }

        public float ConsumptionRate
        {
            get => consumptionRate;
            set => consumptionRate = value;
        }

        public float RecoveryRate
        {
            get => recoveryRate;
            set => recoveryRate = value;
        }

        public float MinValue
        {
            get => minValue;
            set => minValue = value;
        }
    }
}