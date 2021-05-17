using System.Linq;
using Gravitas.DAL;
using Gravitas.Model;
using Gravitas.Platform.Web.ViewModel.RouteEditor;

namespace Gravitas.Platform.Web.Manager.Routes
{
    public class RoutesWebManager : IRoutesWebManager
    {
        private readonly IRoutesRepository _routesRepository;

        public RoutesWebManager(IRoutesRepository routesRepository)
        {
            _routesRepository = routesRepository;
        }

        public RoutesListVm GetRoutesListVm()
        {
            var vm = new RoutesListVm
            {
                Routes = _routesRepository.GetQuery<RouteTemplate, long>().ToList()
                    .Select(item => new RoutesListItemVm
                    {
                        Id = item.Id,
                        Name = item.Name
                    })
            };

            return vm;
        }
    }
}