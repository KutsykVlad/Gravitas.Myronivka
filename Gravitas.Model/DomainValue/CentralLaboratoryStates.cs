namespace Gravitas.Model.DomainValue
{
    public enum CentralLabState
    {
        InQueue = 0,
        SecurityEntry = 1,
        Weighbridge = 2,
        Loaded = 3,
        WaitForSamplesCollect = 4,
        SamplesCollected = 5,
        WaitForOperator = 6,
        OnCollision = 7,
        CollisionApproved = 8, 
        CollisionDisapproved = 9,
        ReturnToCollectSamples = 10,
        Moved = 11,
        Processed = 12
    }
}