using System.ComponentModel.DataAnnotations.Schema;

namespace Gravitas.Model.DomainModel.PreRegistration.DAO
{
    [Table("PreRegisterProduct")]
    public class PreRegisterProduct : BaseEntity<long>
    {
        public long RouteTemplateId { get; set; }
        public string Title { get; set; }
        public int RouteTimeInMinutes { get; set; }

        public virtual RouteTemplate RouteTemplate { get; set; }
    }
}