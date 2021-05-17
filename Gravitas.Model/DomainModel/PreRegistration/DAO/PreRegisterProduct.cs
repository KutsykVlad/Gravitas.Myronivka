using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.PredefinedRoute.DAO;

namespace Gravitas.Model.DomainModel.PreRegistration.DAO
{
    [Table("PreRegisterProduct")]
    public class PreRegisterProduct : BaseEntity<int>
    {
        public int RouteTemplateId { get; set; }
        public string Title { get; set; }
        public int RouteTimeInMinutes { get; set; }

        public virtual RouteTemplate RouteTemplate { get; set; }
    }
}