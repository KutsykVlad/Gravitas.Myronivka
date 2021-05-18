using System.Collections.Generic;
using Gravitas.DAL.DbContext;
using Gravitas.Model;
using Gravitas.Model.DomainModel.PredefinedRoute.DAO;

namespace Gravitas.DAL
{
    public interface IRoutesRepository : IBaseRepository<GravitasDbContext>
    {
        ICollection<RouteTemplate> GetRoutes();
        RouteTemplate GetRoute(long id);
        bool DeleteRoute(long id);
        bool UpdateRoute(RouteTemplate route);
    }
}