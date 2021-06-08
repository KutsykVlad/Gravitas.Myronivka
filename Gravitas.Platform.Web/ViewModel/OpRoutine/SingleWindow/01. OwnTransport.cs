using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class SingleWindowVms
    {
        public class OwnTransportVm
        {
            public int NodeId { get; set; }
            public List<OwnTransportViewModel> Items { get; set; }
        }
    }

    public class OwnTransportViewModel
    {
        [DisplayName("Номер картки")]
        public int CardNo { get; set; }
        
        [DisplayName("Long range (ID)")]
        public string LongRangeCardId { get; set; }
        
        [DisplayName("Номер авто")]
        public string TruckNo { get; set; }
        
        [DisplayName("Номер причепа")]
        public string TrailerNo { get; set; }
        
        [DisplayName("Водій")]
        public string Driver { get; set; }
        
        [DisplayName("Тип")]
        public string TypeId { get; set; }
        
        [DisplayName("Дійсна до")]
        public DateTime ExpirationDate { get; set; }
    }
}