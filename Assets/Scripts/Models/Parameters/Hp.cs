using System;

namespace Models.Parameters
{
    [Serializable]
    public class Hp : ParameterBase
    {
        public Hp()
        {
            Value = 100;
            MaxValue = 100;
            MinValue = 0;
            ConsumptionRate = 0;
            RecoveryRate = 1f;
        }
    }
}