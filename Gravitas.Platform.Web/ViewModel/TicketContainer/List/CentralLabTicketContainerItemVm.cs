using System;
using System.ComponentModel;
using Gravitas.Model.DomainValue;

namespace Gravitas.Platform.Web.ViewModel.TicketContainer.List
{
    public class CentralLabTicketContainerItemVm
    {
        public Guid Id { get; set; }
        
        [DisplayName("Картка")]
        public string Card { get; set; }
        [DisplayName("Транспорт No.")]
        public string TransportNo { get; set; }
        [DisplayName("Причеп No.")]
        public string TrailerNo { get; set; }
        [DisplayName("Продукт")]
        public string ProductName { get; set; }
        [DisplayName("Час відбору проби /початок/")]
        public DateTime? SampleCheckInDateTime { get; set; }
        [DisplayName("Час відбору проби /кінець/")]
        public DateTime? SampleCheckOutDateTime { get; set; }
        [DisplayName("Поточний стан")]
        public string State { get; set; }

        public string ClassStyle { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
    }
}