namespace Gravitas.Model.DomainModel.PackingTare.DTO
{
    public class TicketPackingTareVm
    {
        public long TicketId { get; set; }
        public long PackingId { get; set; }
        public string PackingTitle { get; set; }
        public int Count { get; set; }
    }
}