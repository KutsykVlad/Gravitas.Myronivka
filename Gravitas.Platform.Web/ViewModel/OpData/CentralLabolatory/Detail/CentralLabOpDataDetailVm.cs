using System;
using System.ComponentModel;
using Gravitas.Model.DomainValue;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class CentralLabVms
    {
        public class CentralLabOpDataOpDataDetailVm : BaseOpDataDetailVm
        {
            [DisplayName("Час відбору проби /початок/")]
            public DateTime? SampleCheckInDateTime { get; set; }
            [DisplayName("Час відбору проби /кінець/")]
            public DateTime? SampleCheckOutDateTime { get; set; }

            [DisplayName("Поточний стан")]
            public string State { get; set; }
            [DisplayName("Продукт")]
            public string ProductName { get; set; }
            [DisplayName("Отримувач")]
            public string ReceiverName { get; set; }
            [DisplayName("Коментар лаборатна")]
            public string LabComment { get; set; }

            [DisplayName("Транспорт No.")]
            public string TransportNo { get; set; }
            [DisplayName("Причеп No.")]
            public string TrailerNo { get; set; }

            public bool IsActive { get; set; }
            [DisplayName("Коментар майстра")]
            public string CollisionComment { get; set; }
            public CentralLabState CentralLabState { get; set; }
        }
    }
}
