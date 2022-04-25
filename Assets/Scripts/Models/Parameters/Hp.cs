using System;

namespace Models.Parameters
{
    [Serializable]
    public class Hp : ParameterBase
    {
        public Hp()
        {
            value = 100;
            maxValue = 100;
            minValue = 0;
            consumptionRate = 0;
            recoveryRate = 1f;
        }
    }
}