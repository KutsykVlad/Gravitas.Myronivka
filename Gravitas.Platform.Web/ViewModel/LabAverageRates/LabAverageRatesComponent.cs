using System;
using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel.LabAverageRates
{
    public class LabAverageRatesComponent
    {
        [DisplayName("Дата")]
        public DateTime? CheckOutDateTime { get; set; }
        [DisplayName("Номер автомобіля")]
        public string TransportNo { get; set; }
        [DisplayName("Номер причепа")]
        public string TrailerNo { get; set; }
        [DisplayName("Вол.")]
        public float? ImpurityValue { get; set; }
        [DisplayName("Дом.")]
        public float? HumidityValue { get; set; }
        [DisplayName("Прот.")]
        public float? EffectiveValue { get; set; }
    }
}