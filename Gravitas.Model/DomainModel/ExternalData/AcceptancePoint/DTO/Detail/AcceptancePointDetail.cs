﻿using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.AcceptancePoint.DTO.Detail
{
    public class AcceptancePointDetail : BaseEntity<string>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}