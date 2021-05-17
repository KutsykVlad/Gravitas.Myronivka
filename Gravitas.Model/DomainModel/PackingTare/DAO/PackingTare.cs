using System.ComponentModel.DataAnnotations.Schema;

namespace Gravitas.Model.DomainModel.PackingTare.DAO
{
    [Table("PackingTare")]
    public class PackingTare : BaseEntity<long>
    {
        public string Title { get; set; }
        public float Weight { get; set; }
    }
}
