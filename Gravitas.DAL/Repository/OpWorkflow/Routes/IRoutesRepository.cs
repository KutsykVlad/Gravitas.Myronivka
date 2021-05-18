using System.Collections.Generic;
using Gravitas.DAL.Repository._Base;
using Gravitas.Model.DomainModel.PredefinedRoute.DAO;

namespace Gravitas.DAL.Repository.OpWorkflow.Routes
{
    public interface IRoutesRepository : IBaseRepository
    {
        ICollection<RouteTemplate> GetRoutes();
        RouteTemplate GetRoute(int id);
        bool DeleteRoute(int id);
        bool UpdateRoute(RouteTemplate route);
    }
}