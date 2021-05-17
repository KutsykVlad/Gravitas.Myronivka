﻿using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;
using ExternalData = Gravitas.Model.DomainModel.ExternalData.AcceptancePoint.DAO.ExternalData;

namespace Gravitas.DAL.Mapping {

	public static partial class ExternalDataMap {

		public class BudgetMap : EntityTypeConfiguration<ExternalData.Budget> {

			public BudgetMap() {
				this.ToTable("ext.Budget");

				this.HasKey(e => e.Id);

				this.Property(e => e.Id)
					.HasMaxLength(250);

				this.Property(e => e.Code)
					.HasMaxLength(250);

				this.Property(e => e.Name)
					.HasMaxLength(250);

				this.Property(e => e.ParentId)
					.HasMaxLength(250);
			}
		}
	}
}
