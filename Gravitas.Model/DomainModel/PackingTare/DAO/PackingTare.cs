using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.PackingTare.DAO
{
    [Table("PackingTare")]
    public class PackingTare : BaseEntity<int>
    {
        public string Title { get; set; }
        public float Weight { get; set; }
    }
}
