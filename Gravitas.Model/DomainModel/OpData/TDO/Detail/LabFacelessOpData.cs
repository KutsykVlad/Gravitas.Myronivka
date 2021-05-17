using System;
using System.Collections.Generic;
using Gravitas.Model.DomainModel.OpData.TDO.Json;
using Gravitas.Model.Dto;

namespace Gravitas.Model.DomainModel.OpData.TDO.Detail
{
    public class LabFacelessOpData : BaseOpDataDetail
    {
        public string ImpurityClassId { get; set; }
        public float? ImpurityValue { get; set; }
        public string HumidityClassId { get; set; }
        public float? HumidityValue { get; set; }
        public string InfectionedClassId { get; set; }
        public float? EffectiveValue { get; set; }
        public string Comment { get; set; }
        public string DataSourceName { get; set; }

        public ICollection<LabFacelessOpDataComponent> LabFacelessOpDataItemSet { get; set; }
    }

    public class LabFacelessOpDataComponent
    {
        public int Id { get; set; }
        public DateTime? CheckInDateTime { get; set; }
        public Guid LabFacelessOpDataId { get; set; }
        public int StateId { get; set; }
        public int NodeId { get; set; }
        public string AnalysisTrayRfid { get; set; }
        public AnalysisValueDescriptor AnalysisValueDescriptor { get; set; }
        public string ImpurityClassId { get; set; }
        public float? ImpurityValue { get; set; }
        public string HumidityClassId { get; set; }
        public float? HumidityValue { get; set; }
        public string InfectionedClassId { get; set; }
        public float? EffectiveValue { get; set; }
        public string Comment { get; set; }
        public string DataSourceName { get; set; }
    }
}