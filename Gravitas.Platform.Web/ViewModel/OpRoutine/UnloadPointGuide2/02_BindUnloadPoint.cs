using System.Collections.Generic;
using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel.OpRoutine.UnloadPointGuide2
{
    public static partial class UnloadPointGuide2Vms
    {
        public class BindUnloadPointVm
        {
            public long NodeId { get; set; }

            [DisplayName("Продукт")] public string ProductName { get; set; }
            [DisplayName("Авто")] public string TransportNo { get; set; }
            [DisplayName("Причіп")] public string TrailerNo { get; set; }
            [DisplayName("Відправник")] public string SenderName { get; set; }
            [DisplayName("Перевізник")] public bool IsThirdPartyCarrier { get; set; }

            [DisplayName("Домішки")] public float? ImpurityValue { get; set; }
            [DisplayName("Вологість")] public float? HumidityValue { get; set; }
            [DisplayName("Протеїн")] public float? EffectiveValue { get; set; }

            [DisplayName("Коментар")] public string Comment { get; set; }
            [DisplayName("Коментар лабораторії")] public string LabComment { get; set; }

            [DisplayName("Пункт розвантаження")] public long UnloadNodeId { get; set; }
            [DisplayName("Пункт розвантаження")] public string UnloadNodeName { get; set; }

            public List<VirtualUnloadPoint> NodeItems = new List<VirtualUnloadPoint>
            {
                new VirtualUnloadPoint
                {
                    Id = 1,
                    Title = "Авторозвантажувач #1"
                },
                new VirtualUnloadPoint
                {
                    Id = 2,
                    Title = "Авторозвантажувач #2"
                },
                new VirtualUnloadPoint
                {
                    Id = 3,
                    Title = "Авторозвантажувач #3"
                },
                new VirtualUnloadPoint
                {
                    Id = 4,
                    Title = "Авторозвантажувач #4"
                }
            };
        }
        
        public class VirtualUnloadPoint
        {
            public int Id { get; set; }
            public string Title { get; set; }
        }
    }
}