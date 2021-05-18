using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gravitas.DAL.Mapping.OpDataEvent
{
    class OpDataEventMap : EntityTypeConfiguration<Model.DomainModel.OpDataEvent.DAO.OpDataEvent>
    {
        public OpDataEventMap()
        {
            ToTable("OpDataEvent");

            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}