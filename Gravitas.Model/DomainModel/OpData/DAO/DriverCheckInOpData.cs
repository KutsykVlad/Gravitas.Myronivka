using System;
using System.ComponentModel.DataAnnotations;

namespace Gravitas.Model.DomainModel.OpData.DAO
{
    public class DriverCheckInOpData
    {
        [Key]
        public int Id { get; set; }
        public int NodeId { get; set; }
        public int OrderNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Driver { get; set; }
        public string Truck { get; set; }
        public string Trailer { get; set; }
        public int? DriverPhotoId { get; set; }
        public DateTime? CheckInDateTime { get; set; }
        public int? TicketId { get; set; }
        public bool IsInvited { get; set; }
        
        public virtual DriverPhoto.DAO.DriverPhoto DriverPhoto { get; set; }
        public virtual Ticket.DAO.Ticket Ticket { get; set; }
        public virtual Node.DAO.Node Node { get; set; }
    }
}