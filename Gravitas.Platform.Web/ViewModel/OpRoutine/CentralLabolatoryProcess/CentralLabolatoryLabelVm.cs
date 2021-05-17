using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class CentralLaboratoryProcess
    {
        public class CentralLaboratoryLabelVm
        {
            [DisplayName("Час відбору проби")]
            public DateTime? CheckOutDateTime { get; set; }

            [DisplayName("Продукт")] 
            public string ProductName { get; set; }
            [DisplayName("Відправник")] 
            public string SenderName { get; set; }
            [DisplayName("Транспорт No.")] 
            public string TransportNo { get; set; }
            [DisplayName("Причеп No.")] 
            public string TrailerNo { get; set; }

            [DisplayName("Проба відібрана")] 
            public string SampleCollectEmployee { get; set; }
            [DisplayName("Технік-лаборант")]
            public string LabProcessEmployee { get; set; }
        }
    }
}