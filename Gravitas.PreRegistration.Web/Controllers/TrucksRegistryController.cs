using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Gravitas.PreRegistration.Web.Models;
using Gravitas.PreRegistration.Web.Models.Administrative;
using Gravitas.PreRegistration.Web.Models.Registry;
using Microsoft.AspNet.Identity;

namespace Gravitas.PreRegistration.Web.Controllers
{
    public class TrucksRegistryController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        // GET: TrucksRegistry
        public ActionResult SetRoute()
        {
            if (User.IsInRole("admin"))
            {
                return RedirectToAction("AdminManage");
            }
            else
            {
                return RedirectToAction("UserManage");
            }
        }

        [System.Web.Mvc.Authorize(Roles = "admin")]
        public ActionResult AdminManage()
        {
            var vm = new UsersVm();
            vm.Items = _context.Users.Select(t => new UserInfoVm()
            {
                Id = t.Id,
                PartnerName = t.PartnerName,
                Email = t.Email,
                IsAllowedToRegister = t.IsRegistrationAllowed
            }).ToList();

            return View(vm);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult AdminManagePost(UsersVm vm)
        {
            foreach(var user in vm.Items)
            {
                _context.Users.Find(user.Id).IsRegistrationAllowed = user.IsAllowedToRegister;
            }

            _context.SaveChanges();
            return RedirectToAction("AdminManage");
        }

        [System.Web.Mvc.Authorize(Roles = "user")]
        public ActionResult UserManage()
        {
            var user = _context.Users.Find(User.Identity.GetUserId());
            ViewBag.allowed = user.IsRegistrationAllowed;
            var vm = new UserTruckRegistryVm();
            vm.PartnerId = user.PartnerId;
            vm.Items = _context.RegisteredTrucks.Where(truck => truck.PartnerId == vm.PartnerId).Select(t =>
                new RegistryItemVm()
                {
                    Id = t.Id.ToString(),
                    EntranceTime = t.EntranceTime,
                    RouteId = t.RouteId,
                    PhoneNo = t.PhoneNo,
                    TrailerNo = t.TrailerNo,
                    TruckNo = t.TruckNo
                }).ToList();

            return View(vm);
        }

        
    }
}