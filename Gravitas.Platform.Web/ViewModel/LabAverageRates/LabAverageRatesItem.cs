using System.Collections.Generic;
using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel.LabAverageRates
{
    public class LabAverageRatesItem
    {
        [DisplayName("Господарство")]
        public string PartnerName { get; set; }
        [DisplayName("Номенклатура")]
        public string Nomenclature { get; set; }
        [DisplayName("Класифікатор")]
        public string Classifier { get; set; }
        [DisplayName("Вол.")]
        public float? ImpurityValue { get; set; }
        [DisplayName("Дом.")]
        public float? HumidityValue { get; set; }
        [DisplayName("Прот.")]
        public float? EffectiveValue { get; set; }
        
        public List<LabAverageRatesComponent> Components { get; set; }
    }
}