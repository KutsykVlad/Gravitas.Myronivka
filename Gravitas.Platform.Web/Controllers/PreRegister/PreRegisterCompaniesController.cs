using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gravitas.DAL.Repository.PreRegistration;
using Gravitas.Model.DomainModel.PreRegistration.DAO;
using Gravitas.Platform.Web.ViewModel.PreRegistration.Company;

namespace Gravitas.Platform.Web.Controllers.PreRegister
{
    public class PreRegisterCompaniesController : Controller
    {
        private readonly IPreRegistrationRepository _preRegistrationRepository;

        public PreRegisterCompaniesController(IPreRegistrationRepository preRegistrationRepository)
        {
            _preRegistrationRepository = preRegistrationRepository;
        }

        public ActionResult Get()
        {
            var list = _preRegistrationRepository.GetQuery<PreRegisterCompany, int>()
                .Select(x => new PreRegisterCompanyVm
                {
                    AllowToAdd = x.AllowToAdd,
                    Email = x.Email,
                    TrucksMax = x.TrucksMax,
                    TrucksInProgress = x.TrucksInProgress,
                    CompanyName = x.Name,
                    ContactPhoneNo = x.ContactPhoneNumber,
                    EnterpriseCode = x.EnterpriseCode
                })
                .OrderBy(e => e.Email)
                .ToList();
            return View(list);
        }
        
        [HttpGet]
        public ActionResult Update(IEnumerable<PreRegisterCompanyVm> items)
        {
            try
            {
                foreach (var company in items)
                {
                    var c = _preRegistrationRepository.FindCompanyByUserName(company.Email);
                    c.AllowToAdd = company.AllowToAdd;
                    c.TrucksMax = company.TrucksMax;
                    _preRegistrationRepository.Update<PreRegisterCompany, int>(c);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new HttpStatusCodeResult(500, e.Message);
            }

            return RedirectToAction(nameof(Get));
        }
    }
}