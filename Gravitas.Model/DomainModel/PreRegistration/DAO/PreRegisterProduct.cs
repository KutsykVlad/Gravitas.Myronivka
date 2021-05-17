using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.PreRegistration.DAO
{
    [Table("PreRegisterProduct")]
    public class PreRegisterProduct : BaseEntity<int>
    {
        public long RouteTemplateId { get; set; }
        public string Title { get; set; }
        public int RouteTimeInMinutes { get; set; }

        public virtual RouteTemplate RouteTemplate { get; set; }
    }
}