using System;
using System.Collections.Generic;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.Model;
using Gravitas.Model.DomainModel.PredefinedRoute.DAO;
using NLog;

namespace Gravitas.DAL
{
    public class RoutesRepository : BaseRepository<GravitasDbContext>, IRoutesRepository
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly GravitasDbContext _context;
        public RoutesRepository(GravitasDbContext context) : base(context)
        {
            _context = context;
        }

        public RouteTemplate GetRoute(long id)
        {
            return _context.RouteTemplates.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<RouteTemplate> GetRoutes()
        {
            return GetQuery<RouteTemplate, long>().ToList();
        }

        public bool DeleteRoute(long id)
        {
            var route = GetRoute(id);
            try
            {
                Delete<RouteTemplate, long>(route);
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
                AddOrUpdate<RouteTemplate, long>(route);
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