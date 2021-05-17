using Gravitas.Model;
using Gravitas.Model.DomainModel.OpData.DAO;

namespace Gravitas.DAL.Mapping {

	class LabFacelessOpDataComponentMap : BaseEntityMap<LabFacelessOpDataComponent, long> {

		public LabFacelessOpDataComponentMap() {
			this.ToTable("opd.LabFacelessOpDataComponent");

			this.HasMany(e => e.OpVisaSet)
				.WithOptional(e => e.LabFacelessOpDataComponent)
				.HasForeignKey(e => e.LabFacelessOpDataComponentId);
		}
	}
}
