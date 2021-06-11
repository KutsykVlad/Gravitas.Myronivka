namespace Gravitas.Model.DomainValue
{
    public enum MessageStatus
    {
        Unknown = 0,
        Rejected = 1,
        Undeliverable = 2,
        Deleted = 3,
        Expired = 4,
        Delivered = 5,
        Enroute = 6,
        Accepted = 7
    }
}