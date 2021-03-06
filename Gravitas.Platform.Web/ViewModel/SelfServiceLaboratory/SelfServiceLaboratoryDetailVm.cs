using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel.SelfServiceLaboratory
{
    public class SelfServiceLaboratoryDetailVm
    {
        [DisplayName("Картка")]
        public string CardNumber { get; set; }

        [DisplayName("Продукт")]
        public string ProductName { get; set; }

        [DisplayName("Транспорт")]
        public string TransportNo { get; set; }

        [DisplayName("Причеп")]
        public string TrailerNo { get; set; }

        [DisplayName("Перевізник")]
        public string PartnerName { get; set; }
        
        [DisplayName("Дом %")]
        public float? ImpurityValue { get; set; }
        [DisplayName("Вол %")]
        public float? HumidityValue { get; set; }
        [DisplayName("Прот.")]
        public float? EffectiveValue { get; set; }
        [DisplayName("Зараженість")]
        public string IsInfectioned { get; set; }
        [DisplayName("Коментар")]
        public string Comment { get; set; }
        
    }
}