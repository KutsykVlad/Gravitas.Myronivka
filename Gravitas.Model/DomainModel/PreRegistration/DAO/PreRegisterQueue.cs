using System;
using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.PredefinedRoute.DAO;

namespace Gravitas.Model.DomainModel.PreRegistration.DAO
{
    [Table("PreRegisterQueue")]
    public class PreRegisterQueue : BaseEntity<int>
    {
        public string PhoneNo { get; set; }
        public int PreRegisterCompanyId { get; set; }
        public int RouteTemplateId { get; set; }
        public DateTime RegisterDateTime { get; set; }
        public string TruckNumber { get; set; }
        public string Notice { get; set; }

        public virtual PreRegisterCompany PreRegisterCompany { get; set; }
        public virtual RouteTemplate RouteTemplate { get; set; }
    }
}