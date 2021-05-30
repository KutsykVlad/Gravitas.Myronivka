using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.YearOfHarvest.DAO
{
    public class YearOfHarvest : BaseEntity<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsFolder { get; set; }
        public Guid? ParentId { get; set; }
    }
}