using System;
using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.OpData.DAO.Base;

namespace Gravitas.Model.DomainModel.OpData.DAO
{
    public class LabFacelessOpData : BaseOpData
    {
        public string ImpurityClassId { get; set; }
        public float? ImpurityValue { get; set; }
        public string HumidityClassId { get; set; }
        public float? HumidityValue { get; set; }
        public string InfectionedClassId { get; set; }
        public float? EffectiveValue { get; set; }
        public string Comment { get; set; }
        public string DataSourceName { get; set; }

        public int? ImpurityValueSourceId { get; set; }
        public int? HumidityValueSourceId { get; set; }
        public int? InfectionedValueSourceId { get; set; }
        public int? EffectiveValueSourceId { get; set; }

        public string CollisionComment { get; set; }
        public string LabEffectiveClassifier { get; set; }

        public virtual ICollection<LabFacelessOpDataComponent> LabFacelessOpDataComponentSet { get; set; }
    }

    public class LabFacelessOpDataComponent : BaseEntity<int>
    {
        public Guid LabFacelessOpDataId { get; set; }
        public DomainValue.OpDataState StateId { get; set; }
        public int NodeId { get; set; }
        public string AnalysisTrayRfid { get; set; }
        public string AnalysisValueDescriptor { get; set; }
        public DateTime? CheckInDateTime { get; set; }
        public string ImpurityClassId { get; set; }
        public float? ImpurityValue { get; set; }
        public string HumidityClassId { get; set; }
        public float? HumidityValue { get; set; }
        public string InfectionedClassId { get; set; }
        public float? EffectiveValue { get; set; }
        public string Comment { get; set; }
        public string DataSourceName { get; set; }

        public virtual LabFacelessOpData LabFacelessOpData { get; set; }
        public virtual ICollection<OpVisa.DAO.OpVisa> OpVisaSet { get; set; }
        public virtual Node.DAO.Node Node { get; set; }
    }
}