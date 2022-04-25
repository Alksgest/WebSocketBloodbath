using System;

namespace Models.Parameters
{
    [Serializable]
    public class Stamina : ParameterBase
    {
        public Stamina()
        {
            value = 100;
            maxValue = 100;
            minValue = 0;
            consumptionRate = 10f;
            recoveryRate = 2f;
        }
    }
}