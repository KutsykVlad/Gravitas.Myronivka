using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.LabClassifier.DAO
{
    public class LabDeviceResultType : BaseEntity<Guid>
    {
        public string Name { get; set; }
    }
}