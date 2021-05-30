using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.Crop.DTO
{
    public class CropDetail : BaseEntity<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}