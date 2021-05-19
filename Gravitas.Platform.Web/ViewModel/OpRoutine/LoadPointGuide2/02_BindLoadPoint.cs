using System.Collections.Generic;
using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel.OpRoutine.LoadPointGuide2
{
    public static partial class LoadPointGuide2Vms
    {
        public class BindDestPointVm
        {
            public int NodeId { get; set; }

            [DisplayName("Картка")]
            public string Card { get; set; }

            [DisplayName("Номенклатура")]
            public string ProductName { get; set; }

            [DisplayName("Авто")]
            public string TransportNo { get; set; }

            [DisplayName("Причіп")]
            public string TrailerNo { get; set; }

            [DisplayName("Отримувач")]
            public string ReceiverName { get; set; }

            [DisplayName("Перевізник")]
            public bool IsThirdPartyCarrier { get; set; }

            [DisplayName("Точка призначення")]
            public int DestNodeId { get; set; }

            public string DestNodeName { get; set; }

            [DisplayName("Норматив завантаження/ Довант./ Част. розвант.")]
            public double WeightValue { get; set; }

            [DisplayName("Стороння тара")]
            public double? PackingWeightValue { get; set; }

            [DisplayName("Плюс, кг.")]
            public int LoadTargetDeviationPlus { get; set; }

            [DisplayName("Мінус, кг.")]
            public int LoadTargetDeviationMinus { get; set; }
            public List<VirtualUnloadPoint> NodeItems = new List<VirtualUnloadPoint>
            {
                new VirtualUnloadPoint
                {
                    Id = 1,
                    Title = "Склад #1"
                },
                new VirtualUnloadPoint
                {
                    Id = 2,
                    Title = "Склад #2"
                },
                new VirtualUnloadPoint
                {
                    Id = 3,
                    Title = "Склад #3"
                },
                new VirtualUnloadPoint
                {
                    Id = 4,
                    Title = "Склад #4"
                },
                new VirtualUnloadPoint
                {
                    Id = 5,
                    Title = "Склад #5"
                },
                new VirtualUnloadPoint
                {
                    Id = 6,
                    Title = "Склад #6"
                },
                new VirtualUnloadPoint
                {
                    Id = 7,
                    Title = "Склад #7"
                },
                new VirtualUnloadPoint
                {
                    Id = 10,
                    Title = "Склад #10"
                },
                new VirtualUnloadPoint
                {
                    Id = 11,
                    Title = "Склад #11"
                },
                new VirtualUnloadPoint
                {
                    Id = 12,
                    Title = "Склад #12"
                },
                new VirtualUnloadPoint
                {
                    Id = 13,
                    Title = "Склад #13"
                },
                new VirtualUnloadPoint
                {
                    Id = 14,
                    Title = "Склад #14"
                },
                new VirtualUnloadPoint
                {
                    Id = 15,
                    Title = "Склад #15"
                },
                new VirtualUnloadPoint
                {
                    Id = 16,
                    Title = "Склад #16"
                },
                new VirtualUnloadPoint
                {
                    Id = 17,
                    Title = "Склад #17"
                },
                new VirtualUnloadPoint
                {
                    Id = 19,
                    Title = "Склад #19"
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