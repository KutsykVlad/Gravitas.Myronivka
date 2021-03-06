using System;
using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.OpData.DAO;

namespace Gravitas.Model.DomainModel.OpCameraImage
{
    public class OpCameraImage : BaseEntity<int>
    {
        public string Source { get; set; }
        public string ImagePath { get; set; }
        public DateTime? DateTime { get; set; }

        public int DeviceId { get; set; }

        public Guid? LabFacelessOpDataId { get; set; }
        public Guid? LabRegularOpDataId { get; set; }
        public Guid? LoadOpDataId { get; set; }
        public Guid? SingleWindowOpDataId { get; set; }
        public Guid? SecurityCheckInOpDataId { get; set; }
        public Guid? SecurityCheckOutOpDataId { get; set; }
        public Guid? ScaleOpDataId { get; set; }
        public Guid? UnloadGuideOpDataId { get; set; }
        public Guid? UnloadPointOpDataId { get; set; }
        public Guid? LoadGuideOpDataId { get; set; }
        public Guid? LoadPointOpDataId { get; set; }
        public Guid? NonStandartOpDataId { get; set; }

        public virtual Device.DAO.Device Device { get; set; }

        public virtual LabFacelessOpData LabFacelessOpData { get; set; }
        public virtual SingleWindowOpData SingleWindowOpData { get; set; }
        public virtual SecurityCheckInOpData SecurityCheckInOpData { get; set; }
        public virtual SecurityCheckOutOpData SecurityCheckOutOpData { get; set; }
        public virtual ScaleOpData ScaleOpData { get; set; }
        public virtual UnloadGuideOpData UnloadGuideOpData { get; set; }
        public virtual UnloadPointOpData UnloadPointOpData { get; set; }
        public virtual LoadGuideOpData LoadGuideOpData { get; set; }
        public virtual LoadPointOpData LoadPointOpData { get; set; }
        public virtual NonStandartOpData NonStandartOpData { get; set; }
    }
}