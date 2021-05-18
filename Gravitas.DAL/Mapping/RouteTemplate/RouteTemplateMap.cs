using Gravitas.DAL.Mapping._Base;

namespace Gravitas.DAL.Mapping.RouteTemplate
{
    class RouteTemplateMap : BaseEntityMap<Model.DomainModel.PredefinedRoute.DAO.RouteTemplate, int>
    {
        public RouteTemplateMap()
        {
            ToTable("RouteTemplate");

            Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(250);
        }
    }
}