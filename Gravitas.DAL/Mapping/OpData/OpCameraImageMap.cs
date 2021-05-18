using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.OpCameraImage;

namespace Gravitas.DAL.Mapping.OpData
{
    class OpCameraImageMap : EntityTypeConfiguration<OpCameraImage>
    {
        public OpCameraImageMap()
        {
            ToTable("OpCameraImage");

            HasRequired(e => e.Device)
                .WithMany(e => e.CameraImageSet)
                .HasForeignKey(e => e.SourceDeviceId);
        }
    }
}