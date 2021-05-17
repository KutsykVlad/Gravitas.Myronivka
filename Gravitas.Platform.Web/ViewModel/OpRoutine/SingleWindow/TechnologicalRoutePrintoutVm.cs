using System;
using System.Collections.Generic;
using System.ComponentModel;
using Gravitas.Infrastructure.Platform.Manager.Routes;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class SingleWindowVms
    {
        public class TechnologicalRoutePrintoutVm
        {
            [DisplayName("Номенклатура")]
            public string ProductName { get; set; }
            [DisplayName("Відправник")]
            public string SenderName { get; set; }
            [DisplayName("Водій")]
            public string Driver { get; set; }
            [DisplayName("Відправник/одержувач")]
            public string ReceiverName { get; set; }
            [DisplayName("Номер вантажівки")]
            public string TruckNo { get; set; }
            [DisplayName("Номер причепу")]
            public string TrailerNo { get; set; }
            [DisplayName("Брутто ")]
            public double? GrossValue { get; set; }
            [DisplayName("Брутто Дата зважування ")]
            public DateTime? GrossDate { get; set; }
            [DisplayName("Тара")]
            public double? TareValue { get; set; }
            [DisplayName("Тара Дата зважування")]
            public DateTime? TareDate { get; set; }
            [DisplayName("Нетто")]
            public double? NetValue { get; set; }
            [DisplayName("Нетто Дата зважування")]
            public DateTime? NetDate { get; set; }
            public DateTime Date { get; set; }
        }
    }
}
