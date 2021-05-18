using System.Linq;
using System.Web.Mvc;
using Gravitas.DAL;
using Gravitas.DAL.Repository.Node;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.DAO;

namespace Gravitas.Platform.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly INodeRepository _nodeRepository;
        public HomeController(INodeRepository nodeRepository)
        {
            _nodeRepository = nodeRepository;
        }

        public ActionResult Index()
        {
            var nodes = _nodeRepository.GetQuery<Node, long>().ToList();
            return View(nodes);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}