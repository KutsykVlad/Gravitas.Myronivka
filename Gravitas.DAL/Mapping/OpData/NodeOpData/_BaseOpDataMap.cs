using System;
using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.Model;
using Gravitas.Model.DomainModel.OpData.DAO.Base;

namespace Gravitas.DAL.Mapping
{
	class BaseOpDataMap<TEntity> : BaseEntityMap<TEntity, Guid> where TEntity : BaseOpData {

		public BaseOpDataMap() {

			this.Property(e => e.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			this.Property(e => e.CheckInDateTime)
				.HasColumnType("datetime2")
				.IsOptional();
			this.Property(e => e.CheckOutDateTime)
				.HasColumnType("datetime2")
				.IsOptional();
		}
	}
}
