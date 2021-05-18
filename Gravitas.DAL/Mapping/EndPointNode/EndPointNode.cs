using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.DAL.Mapping._Base;

namespace Gravitas.DAL.Mapping.EndPointNode
{
    internal class EndPointNodeMap : BaseEntityMap<Model.DomainModel.EndPointNodes.DAO.EndPointNode, int>
    {
        public EndPointNodeMap()
        {
            ToTable("EndPointNode");
            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}