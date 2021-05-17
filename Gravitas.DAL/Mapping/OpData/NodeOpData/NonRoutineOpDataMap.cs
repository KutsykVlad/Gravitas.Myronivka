using Gravitas.Model;
using Gravitas.Model.DomainModel.OpData.DAO;

namespace Gravitas.DAL.Mapping {

	class NonStandartOpDataMap : BaseOpDataMap<NonStandartOpData> {

		public NonStandartOpDataMap() {

			this.ToTable("opd.NonStandartOpData");

			this.HasRequired(e => e.OpDataState)
				.WithMany(e => e.NonStandartOpDataSet)
				.HasForeignKey(e => e.StateId);

			this.HasOptional(e => e.Node)
				.WithMany(e => e.NonStandartOpDataSet)
				.HasForeignKey(e => e.NodeId);
			
			this.HasMany(e => e.OpCameraSet)
				.WithOptional(e => e.NonStandartOpData)
				.HasForeignKey(e => e.NonStandartOpDataId);
		}
	}
}