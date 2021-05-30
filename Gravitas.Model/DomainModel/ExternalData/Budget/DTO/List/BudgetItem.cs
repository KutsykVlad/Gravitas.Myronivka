using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.Budget.DTO.List
{
    public class BudgetItem : BaseEntity<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsFolder { get; set; }
    }
}