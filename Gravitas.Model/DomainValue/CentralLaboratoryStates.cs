namespace Gravitas.Model.DomainValue
{
    public enum CentralLabState
    {
        None = 0,
        InQueue = 1,
        SecurityEntry = 2,
        Weighbridge = 3,
        Loaded = 4,
        WaitForSamplesCollect = 5,
        SamplesCollected = 6,
        WaitForOperator = 7,
        OnCollision = 8,
        CollisionApproved = 9, 
        CollisionDisapproved = 10,
        ReturnToCollectSamples = 11,
        Moved = 12,
        Processed = 13
    }
}