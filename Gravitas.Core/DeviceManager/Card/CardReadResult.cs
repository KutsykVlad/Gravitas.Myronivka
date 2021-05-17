namespace Gravitas.Core.DeviceManager.Card
{
    public class CardReadResult
    {
        public Model.Ticket Ticket { get; set; }
        public bool IsOwn { get; set; }
        public string Id { get; set; }
    }
}