using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using Gravitas.PreRegistration.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Gravitas.PreRegistration.Web.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        public ActionResult Index()
        {
            var user = _context.Users.ToList().FirstOrDefault(t => t.Id == User.Identity.GetUserId());
            ViewBag.allowed = user?.IsRegistrationAllowed;
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Manage()
        {

            return View();
        }

        [Authorize(Roles = "user")]
        public ActionResult Registry()
        {

            return View();
        }

        [Authorize(Roles = "user")]
        public ActionResult RegisterTruck()
        {
            var user = _context.Users.ToList().FirstOrDefault(t => t.Id == User.Identity.GetUserId());
            ViewBag.partnerId = user?.PartnerId;
            ViewBag.partnerName = user?.PartnerName;
            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }
    }
}