using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gravitas.Model.DomainModel.PreRegistration.DAO
{
    [Table("PreRegisterQueue")]
    public class PreRegisterQueue : BaseEntity<long>
    {
        public string PhoneNo { get; set; }
        public long PreRegisterCompanyId { get; set; }
        public long RouteTemplateId { get; set; }
        public DateTime RegisterDateTime { get; set; }
        public string TruckNumber { get; set; }
        public string Notice { get; set; }

        public virtual PreRegisterCompany PreRegisterCompany { get; set; }
        public virtual RouteTemplate RouteTemplate { get; set; }
    }
}