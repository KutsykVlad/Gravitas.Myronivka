using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;

namespace Gravitas.DAL.Mapping
{
	class RouteTemplateMap : BaseEntityMap<RouteTemplate, long> {
		public RouteTemplateMap() {

			this.ToTable("RouteTemplate");

			this.Property(e => e.Name)
				.IsRequired()
				.HasMaxLength(250);

			this.Property(e=>e.CreatedOn).HasColumnType("datetime2");
			this.Property(e => e.UpdatedAt).HasColumnType("datetime2");
		}
	}
}
