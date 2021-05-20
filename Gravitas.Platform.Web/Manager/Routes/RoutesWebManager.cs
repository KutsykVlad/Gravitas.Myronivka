using System.Linq;
using Gravitas.DAL;
using Gravitas.DAL.Repository.OpWorkflow.Routes;
using Gravitas.Model;
using Gravitas.Model.DomainModel.PredefinedRoute.DAO;
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
                Routes = _routesRepository.GetQuery<RouteTemplate, int>().ToList()
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