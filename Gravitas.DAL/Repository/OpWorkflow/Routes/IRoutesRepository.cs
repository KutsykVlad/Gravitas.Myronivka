using System.Collections.Generic;
using Gravitas.Model;

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