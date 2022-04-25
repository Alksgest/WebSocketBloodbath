namespace Models.Parameters
{
    public interface IParameter
    {
        float Value { get; }
        float MaxValue { get; }
        float MinValue { get; }
        float ConsumptionRate { get; }
        float RecoveryRate { get; }
    }
}