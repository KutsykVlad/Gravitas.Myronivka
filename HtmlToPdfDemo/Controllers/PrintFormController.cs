using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HtmlToPdfDemo.Controllers
{
    public class PrintFormController : Controller
    {
        // GET: PrintForm
        [Route("PrintForm", Name = "PrintForm")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
