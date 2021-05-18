using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.DAL.Mapping._Base;
using Gravitas.Model.DomainModel.OpDataState.DAO;

namespace Gravitas.DAL.Mapping.OpData
{
    class OpDataStateMap : BaseEntityMap<OpDataState, int>
    {
        public OpDataStateMap()
        {
            ToTable("OpDataState");

            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}