using System.Linq;
using System.Web.Mvc;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.Node;

namespace Gravitas.Platform.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly GravitasDbContext _context;
        public HomeController(GravitasDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var nodes = _context.Nodes.ToList();
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