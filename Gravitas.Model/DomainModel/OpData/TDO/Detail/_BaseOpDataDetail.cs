using System;

namespace Gravitas.Model.DomainModel.OpData.TDO.Detail
{
    public class BaseOpDataDetail
    {
        public Guid Id { get; set; }
        public DomainValue.OpDataState StateId { get; set; }
        public int? NodeId { get; set; }
        public int? TicketId { get; set; }
        public int? TicketContainerId { get; set; }
        public DateTime? CheckInDateTime { get; set; }
        public DateTime? CheckOutDateTime { get; set; }
    }
}