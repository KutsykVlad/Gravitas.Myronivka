namespace Gravitas.Infrastructure.Platform.Manager.Connect
{
    public class SmsMessageData
    {
        public string DispatcherPhoneNumber { get; set; }
        public string TransportNo { get; set; }
        public string TrailerNo { get; set; }
        public string EntranceTime { get; set; }
        public string ProductName { get; set; }
        public string Email { get; set; }
        public string DestinationPoint { get; set; }
        public string NodeNumber { get; set; }
        public string CollisionComment { get; set; }
        public string CollisionResult { get; set; }
        public string ReceiverName { get; set; }
        public string ScaleValidationText { get; set; }
    }
}