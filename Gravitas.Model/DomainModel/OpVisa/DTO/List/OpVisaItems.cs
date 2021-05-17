using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.OpVisa.DTO.List
{
    public class OpVisaItems : BaseEntity<int>
    {
        public List<OpVisaItem> Items { get; set; }
    }
}