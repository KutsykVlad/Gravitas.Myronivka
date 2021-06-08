using System;
using System.ComponentModel;
using Gravitas.Model.DomainValue;

namespace Gravitas.DAL.Repository.OwnTransport.Models
{
    public class OwnTransportViewModel
    {
        public int? Id { get; set; }
        
        [DisplayName("Номер картки")]
        public int? CardNo { get; set; }
        
        [DisplayName("Long range (ID)")]
        public string LongRangeCardId { get; set; }
        
        [DisplayName("Номер авто")]
        public string TruckNo { get; set; }
        
        [DisplayName("Номер причепа")]
        public string TrailerNo { get; set; }
        
        [DisplayName("Водій")]
        public string Driver { get; set; }
        
        [DisplayName("Тип")]
        public string TypeName { get; set; }
        public OwnTransportType TypeId { get; set; }
        
        [DisplayName("Дійсна до")]
        public DateTime ExpirationDate { get; set; } = DateTime.Now;

        public string ExpirationDateFormatted => ExpirationDate.ToString("dd.MM.yyy");
    }
}