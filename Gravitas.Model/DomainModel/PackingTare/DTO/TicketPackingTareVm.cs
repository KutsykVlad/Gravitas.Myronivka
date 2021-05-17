namespace Gravitas.Model.DomainModel.PackingTare.DTO
{
    public class TicketPackingTareVm
    {
        public int TicketId { get; set; }
        public int PackingId { get; set; }
        public string PackingTitle { get; set; }
        public int Count { get; set; }
    }
}