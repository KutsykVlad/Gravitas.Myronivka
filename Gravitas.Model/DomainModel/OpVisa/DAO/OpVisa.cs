using System;
using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.ExternalData.Employee.DAO;
using Gravitas.Model.DomainModel.MixedFeed.DAO;
using Gravitas.Model.DomainModel.OpData.DAO;

namespace Gravitas.Model.DomainModel.OpVisa.DAO
{
    public class OpVisa : BaseEntity<int>
    {
        public DateTime? DateTime { get; set; }
        public Guid? EmployeeId { get; set; }
        public string Message { get; set; }

        public Guid? LabFacelessOpDataId { get; set; }
        public Guid? LabRegularOpDataId { get; set; }
        public Guid? SingleWindowOpDataId { get; set; }
        public Guid? SecurityCheckInOpDataId { get; set; }
        public Guid? SecurityCheckOutOpDataId { get; set; }
        public Guid? SecurityCheckReviewOpDataId { get; set; }
        public Guid? ScaleTareOpDataId { get; set; }
        public Guid? UnloadPointOpDataId { get; set; }
        public Guid? UnloadGuideOpDataId { get; set; }
        public Guid? LoadPointOpDataId { get; set; }
        public Guid? LoadGuideOpDataId { get; set; }
        public Guid? CentralLaboratoryOpData { get; set; }
        public int? MixedFeedSiloId { get; set; }
        public int OpRoutineStateId { get; set; }
        public int? LabFacelessOpDataComponentId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual LabFacelessOpData LabFacelessOpData { get; set; }
        public virtual SingleWindowOpData SingleWindowOpData { get; set; }
        public virtual SecurityCheckInOpData SecurityCheckInOpData { get; set; }
        public virtual SecurityCheckOutOpData SecurityCheckOutOpData { get; set; }
        public virtual SecurityCheckReviewOpData SecurityCheckReviewOpData { get; set; }
        public virtual ScaleOpData ScaleOpData { get; set; }
        public virtual UnloadPointOpData UnloadPointOpData { get; set; }
        public virtual UnloadGuideOpData UnloadGuideOpData { get; set; }
        public virtual LoadPointOpData LoadPointOpData { get; set; }
        public virtual LoadGuideOpData LoadGuideOpData { get; set; }
        public virtual CentralLabOpData CentralLabOpData { get; set; }
        public virtual MixedFeedSilo MixedFeedSilo { get; set; }
        public virtual LabFacelessOpDataComponent LabFacelessOpDataComponent { get; set; }
    }
}