using System;

namespace Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json
{
    public class LabFossInJsonState
    {
        public DateTime? AnalysisTime { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string SampleType { get; set; }
        public string SampleNumber { get; set; }
        public string SampleComment { get; set; }
        public string InstrumentName { get; set; }
        public string InstrumentSerial { get; set; }
        public double? HumidityValue { get; set; }
        public double? ProteinValue { get; set; }
        public double? OilValue { get; set; }
        public double? FibreValue { get; set; }
    }
}