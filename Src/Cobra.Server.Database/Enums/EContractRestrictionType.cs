namespace Cobra.Server.Database.Enums
{
    [Flags]
    public enum EContractRestrictionType : byte
    {
        None = 0,
        TargetOnly = 1,
        SuitOnly = 2,
        PerfectShooter = 4,
        EraseTraces = 8,
        NoWitnesses = 16
    }
}
