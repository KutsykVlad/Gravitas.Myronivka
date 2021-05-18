using System;
using System.Collections.Generic;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository._Base;
using Gravitas.Model.DomainModel.PredefinedRoute.DAO;
using NLog;

namespace Gravitas.DAL.Repository.OpWorkflow.Routes
{
    public class RoutesRepository : BaseRepository, IRoutesRepository
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly GravitasDbContext _context;
        public RoutesRepository(GravitasDbContext context) : base(context)
        {
            _context = context;
        }

        public RouteTemplate GetRoute(int id)
        {
            return _context.RouteTemplates.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<RouteTemplate> GetRoutes()
        {
            return GetQuery<RouteTemplate, int>().ToList();
        }

        public bool DeleteRoute(int id)
        {
            var route = GetRoute(id);
            try
            {
                Delete<RouteTemplate, int>(route);
                return true;
            }
            catch (Exception e)
            {
                _logger.Error($"DeleteRoute: Error while deleting route: {e}");
                return false;
            }
        }

        public bool UpdateRoute(RouteTemplate route)
        {
            try
            {
                AddOrUpdate<RouteTemplate, int>(route);
                return true;
            }
            catch (Exception e)
            {
                _logger.Error($"UpdateRoute: Error while updating route: {e}");
                return false;
            }
        }
    }
}