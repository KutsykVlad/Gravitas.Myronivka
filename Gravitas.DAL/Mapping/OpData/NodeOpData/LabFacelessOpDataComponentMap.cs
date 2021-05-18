using Gravitas.DAL.Mapping._Base;
using Gravitas.Model.DomainModel.OpData.DAO;

namespace Gravitas.DAL.Mapping.OpData.NodeOpData
{
    class LabFacelessOpDataComponentMap : BaseEntityMap<LabFacelessOpDataComponent, int>
    {
        public LabFacelessOpDataComponentMap()
        {
            ToTable("opd.LabFacelessOpDataComponent");

            HasMany(e => e.OpVisaSet)
                .WithOptional(e => e.LabFacelessOpDataComponent)
                .HasForeignKey(e => e.LabFacelessOpDataComponentId);
        }
    }
}