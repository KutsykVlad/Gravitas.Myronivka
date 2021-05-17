using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.Model;

namespace Gravitas.DAL.Mapping
{
	class OpDataStateMap : BaseEntityMap<OpDataState, long> {
		public OpDataStateMap() {

			this.ToTable("OpDataState");

			this.Property(e => e.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

			this.Property(e => e.Name)
				.IsRequired()
				.HasMaxLength(50);

			//this.HasMany(e => e.SingleWindowOpDataSet)
			//	.WithRequired(e => e.OpDataState)
			//	.HasForeignKey(e => e.StateId);

			//this.HasMany(e => e.SecurityCheckInOpDataSet)
			//	.WithRequired(e => e.OpDataState)
			//	.HasForeignKey(e => e.StateId);

			//this.HasMany(e => e.SecurityCheckOutOpDataSet)
			//	.WithRequired(e => e.OpDataState)
			//	.HasForeignKey(e => e.StateId);

			//this.HasMany(e => e.ScaleOpDataSet)
			//	.WithRequired(e => e.OpDataState)
			//	.HasForeignKey(e => e.StateId);

			//this.HasMany(e => e.LabFacelessOpDataSet)
			//	.WithRequired(e => e.OpDataState)
			//	.HasForeignKey(e => e.StateId);

			//this.HasMany(e => e.LabRegularOpDataSet)
			//	.WithRequired(e => e.OpDataState)
			//	.HasForeignKey(e => e.StateId);

			//this.HasMany(e => e.LoadOpDataSet)
			//	.WithRequired(e => e.OpDataState)
			//	.HasForeignKey(e => e.StateId);

			//this.HasMany(e => e.UnloadOpDataSet)
			//	.WithRequired(e => e.OpDataState)
			//	.HasForeignKey(e => e.StateId);

			//this.HasMany(e => e.NonStandartOpDataSet)
			//	.WithRequired(e => e.OpDataState)
			//	.HasForeignKey(e => e.StateId);
		}
	}
}
