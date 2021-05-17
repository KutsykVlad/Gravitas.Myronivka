using System;

namespace Gravitas.Model.Dto
{
    public class LabInfrascanInJsonState : BaseJsonConverter<LabInfrascanInJsonState>
    {
        public DateTime? AnalysisTime { get; set; }
        public double? HumidityValue { get; set; }
        public double? ProteinValue { get; set; }
        public double? FatValue { get; set; }
        public double? CelluloseValue { get; set; }
    }
}