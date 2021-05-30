using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.FixedAsset.DAO
{
    public class FixedAsset : BaseEntity<Guid>
    {
        public string Code { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string TypeCode { get; set; }
        public string RegistrationNo { get; set; }
        public bool IsFolder { get; set; }
        public Guid? ParentId { get; set; }
    }
}