using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.Model;
using Gravitas.Model.DomainModel.EndPointNodes.DAO;

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