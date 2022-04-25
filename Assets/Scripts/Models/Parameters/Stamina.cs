using System;

namespace Models.Parameters
{
    [Serializable]
    public class Stamina : ParameterBase
    {
        public Stamina()
        {
            Value = 100;
            MaxValue = 100;
            MinValue = 0;
            ConsumptionRate = 10f;
            RecoveryRate = 5f;
        }
    }
}