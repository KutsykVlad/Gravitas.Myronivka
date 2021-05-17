using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.Model;

namespace Gravitas.DAL.Mapping
{
    internal class EndPointNodeMap : BaseEntityMap<EndPointNode, int>
    {
        public EndPointNodeMap()
        {
            ToTable("EndPointNode");
            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}