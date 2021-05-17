using System.Collections.Generic;
using System.ComponentModel;
using Gravitas.Model.DomainModel.Node.TDO.List;
using Gravitas.Model.Dto;

namespace Gravitas.Platform.Web.ViewModel.OpRoutine.MixedFeedGuide
{
    public static partial class MixedFeedGuideVms
    {
        public class BindDestPointVm
        {
            public long NodeId { get; set; }

            [DisplayName("Карта")]
            public string Card { get; set; }
            
            [DisplayName("Продукт")]
            public string ProductName { get; set; }
            
            [DisplayName("Погрузити, кг.")]
            public double LoadTarget { get; set; }

            [DisplayName("Авто")]
            public string TransportNo { get; set; }

            [DisplayName("Отримувач")]
            public string ReceiverName { get; set; }

            [DisplayName("Склад контрагента")]
            public string ReceiverDepotName { get; set; }

            [DisplayName("Ост. опрацьована точка")]
            public string LastNodeName { get; set; }

            [DisplayName("Проїзд для завантаження")]
            public long DestNodeId { get; set; }
            public string DestNodeName { get; set; }
            
            public List<NodeItem> NodeItems { get; set; }
        }
    }
}