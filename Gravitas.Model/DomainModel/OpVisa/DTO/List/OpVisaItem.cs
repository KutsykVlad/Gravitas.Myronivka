using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.OpVisa.DTO.List
{
    public class OpVisaItem : BaseEntity<int>
    {
        public DateTime? DateTime { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
    }
}