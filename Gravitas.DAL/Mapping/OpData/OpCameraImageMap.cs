using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;

namespace Gravitas.DAL.Mapping {

	class OpCameraImageMap : EntityTypeConfiguration<OpCameraImage> {

		public OpCameraImageMap() {

			this.ToTable("OpCameraImage");

			this.HasRequired(e => e.Device)
				.WithMany(e => e.CameraImageSet)
				.HasForeignKey(e => e.SourceDeviceId);

			//this.HasOptional(e => e.SecurityCheckInOpData)
			//	.WithMany(e => e.OpCameraSet)
			//	.HasForeignKey(e => e.SecurityCheckInOpDataId);
		}
	}
}
