﻿using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.LabClassifier.DTO.List
{
    public class LabDeviceResultTypeItem : BaseEntity<string>
    {
        public string Name { get; set; }
    }
}