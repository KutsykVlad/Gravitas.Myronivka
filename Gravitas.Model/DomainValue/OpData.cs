namespace Gravitas.Model.DomainValue
{
    public enum OpDataState
    {
        // Processing states
        Init = 1,
        Processing = 2,
        Collision = 3,
        CollisionApproved = 4,
        CollisionDisapproved = 5,

        // Finished states
        Processed = 10,
        Rejected = 11,
        Canceled = 12,
        PartLoad = 13,
        PartUnload = 14,
        Reload = 15
    }

    public enum ScaleOpDataType
    {
        Tare = 1,
        Gross = 2
    }
}