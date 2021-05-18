using System;
using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.DAL.Mapping._Base;
using Gravitas.Model.DomainModel.OpData.DAO.Base;

namespace Gravitas.DAL.Mapping.OpData.NodeOpData
{
    class BaseOpDataMap<TEntity> : BaseEntityMap<TEntity, Guid> where TEntity : BaseOpData
    {
        public BaseOpDataMap()
        {
            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(e => e.CheckInDateTime)
                .HasColumnType("datetime2")
                .IsOptional();
            
            Property(e => e.CheckOutDateTime)
                .HasColumnType("datetime2")
                .IsOptional();
        }
    }
}