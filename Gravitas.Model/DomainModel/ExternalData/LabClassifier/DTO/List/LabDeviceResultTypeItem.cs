using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.LabClassifier.DTO.List
{
    public class LabDeviceResultTypeItem : BaseEntity<Guid>
    {
        public string Name { get; set; }
    }
}