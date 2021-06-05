using System;
using System.ComponentModel.DataAnnotations;

namespace Gravitas.Model.DomainModel.OpData.DAO
{
    public class DriverCheckInOpData
    {
        [Key]
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Driver { get; set; }
        public string Truck { get; set; }
        public string Trailer { get; set; }
        public int? DriverPhotoId { get; set; }
        public DateTime? CheckInDateTime { get; set; }
        public int? TicketId { get; set; }
        
        public DriverPhoto.DAO.DriverPhoto DriverPhoto { get; set; }
        public Ticket.DAO.Ticket Ticket { get; set; }
    }
}