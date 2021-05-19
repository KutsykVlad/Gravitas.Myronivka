using System.Collections.Generic;
using System.ComponentModel;
using Gravitas.Model.DomainModel.Node.TDO.List;
using Gravitas.Model.Dto;

namespace Gravitas.Platform.Web.ViewModel.OpRoutine.UnloadPointGuide
{
    public static partial class UnloadPointGuideVms
    {
        public class BindUnloadPointVm
        {
            public int NodeId { get; set; }

            [DisplayName("Продукт")] 
            public string ProductName { get; set; }
            [DisplayName("Авто")]
             public string TransportNo { get; set; }
            [DisplayName("Причіп")] 
            public string TrailerNo { get; set; }
            [DisplayName("Відправник")]
            public string SenderName { get; set; }
            [DisplayName("Перевізник")] 
            public bool IsThirdPartyCarrier { get; set; }

            [DisplayName("Домішки")] 
            public float? ImpurityValue { get; set; }
            [DisplayName("Вологість")] 
            public float? HumidityValue { get; set; }
            [DisplayName("Протеїн")] 
            public float? EffectiveValue { get; set; }

            [DisplayName("Коментар")]
            public string Comment { get; set; }
            [DisplayName("Коментар лабораторії")]
            public string LabComment { get; set; }

            [DisplayName("Пункт розвантаження")]
            public int UnloadNodeId { get; set; }
            [DisplayName("Пункт розвантаження")]
            public string UnloadNodeName { get; set; }

            public List<NodeItem> NodeItems { get; set; }
        }
    }
}