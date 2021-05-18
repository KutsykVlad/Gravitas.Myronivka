using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.DAL.Mapping._Base;

namespace Gravitas.DAL.Mapping.PackingTare
{
    class PackingTareMap : BaseEntityMap<Model.DomainModel.PackingTare.DAO.PackingTare, int>
    {
        public PackingTareMap()
        {
            ToTable("PackingTare");
            
            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(e => e.Title)
                .HasMaxLength(100)
                .IsRequired();
            
            HasIndex(x => x.Title)
                .IsUnique();
        }
    }
}