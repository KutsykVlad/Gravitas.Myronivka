using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.Budget.DTO.Detail
{
    public class BudgetDetail : BaseEntity<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}