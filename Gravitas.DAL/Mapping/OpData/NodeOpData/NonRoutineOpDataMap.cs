using Gravitas.Model.DomainModel.OpData.DAO;

namespace Gravitas.DAL.Mapping.OpData.NodeOpData
{
    class NonStandartOpDataMap : BaseOpDataMap<NonStandartOpData>
    {
        public NonStandartOpDataMap()
        {
            ToTable("opd.NonStandartOpData");

            HasRequired(e => e.OpDataState)
                .WithMany(e => e.NonStandartOpDataSet)
                .HasForeignKey(e => e.StateId);

            HasOptional(e => e.Node)
                .WithMany(e => e.NonStandartOpDataSet)
                .HasForeignKey(e => e.NodeId);

            HasMany(e => e.OpCameraSet)
                .WithOptional(e => e.NonStandartOpData)
                .HasForeignKey(e => e.NonStandartOpDataId);
        }
    }
}