using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Gravitas.DAL;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Infrastructure.Platform.ApiClient.OneC;
using Gravitas.Infrastructure.Platform.Manager.Settings;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Card.DAO;
using Gravitas.Model.DomainModel.PhoneDictionary.DAO;
using Gravitas.Platform.Web.Manager;
using Gravitas.Platform.Web.ViewModel;
using Gravitas.Platform.Web.ViewModel.Admin;
using Gravitas.Platform.Web.ViewModel.Admin.Version;
using Dom = Gravitas.Model.DomainValue.Dom;
using ExternalData = Gravitas.Model.DomainModel.ExternalData.AcceptancePoint.DAO.ExternalData;
using Settings = Gravitas.Model.DomainModel.Settings.DAO.Settings;

namespace Gravitas.Platform.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminWebManager _adminWebManager;
        private readonly IExternalDataRepository _externalDataRepository;
        private readonly ICardRepository _cardRepository;
        private readonly ISettings _settings;
        private readonly GravitasDbContext _context;

        public AdminController(IAdminWebManager adminWebManager,
            IExternalDataRepository externalDataRepository,
            ICardRepository cardRepository,
            ISettings settings, 
            GravitasDbContext context)
        {
            _adminWebManager = adminWebManager;
            _externalDataRepository = externalDataRepository;
            _cardRepository = cardRepository;
            _settings = settings;
            _context = context;
        }

        public ActionResult Login()
        {
            return View();  
        }  
  
        [HttpPost]  
        [ValidateAntiForgeryToken]  
        public ActionResult Login(LoginModel model)   
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.Password == GlobalConfigurationManager.AdminPassword)  
            {  
                Session["UserID"] = model.Password;  
                return RedirectToAction("Panel");  
            }  
  
            return View(model);  
        }  
        
        public ActionResult LogOut()   
        {
            Session["UserID"] = null;
            return RedirectToAction(nameof(Login));  
        }  
        
        public ActionResult Panel()
        {
            if (Session["UserID"] == null)  
            {  
                return RedirectToAction("Login");  
            } 
            
            return View();  
        }

        public ActionResult Version()
        {
            var versionVm = new VersionVm
            {
                WebVersion = typeof(VersionVm).Assembly.GetName().Version.ToString()
            };
            return View("Version", versionVm);
        }

        #region Settings

        public ActionResult Settings()
        {
            if (Session["UserID"] == null)  
            {  
                return RedirectToAction("Login");  
            } 
            return View(nameof(Settings), new Settings
            {
                AdminEmail = _settings.AdminEmail,
                LabDataExpireMinutes = _settings.LabDataExpireMinutes,
                QueueDisplayText = _settings.QueueDisplayText,
                QueueEntranceTimeout = _settings.QueueEntranceTimeout,
                PreRegistrationAdminEmail = _settings.PreRegistrationAdminEmail
            });
        }
        
        [HttpPost]
        public ActionResult Settings(Settings settings)
        {
            _settings.AdminEmail = settings.AdminEmail;
            _settings.QueueDisplayText = settings.QueueDisplayText;
            _settings.PreRegistrationAdminEmail = settings.PreRegistrationAdminEmail;
            _settings.Save();
            return RedirectToAction(nameof(Settings));
        }

        #endregion

        #region QueuePatternItems

        public ActionResult QueuePatternItemsTable()
        {
            if (Session["UserID"] == null)  
            {  
                return RedirectToAction("Login");  
            } 
            return View("QueuePattern/QueuePatternItemsTable");
        }

        public ActionResult AddQueuePatternItem()
        {
            if (Session["UserID"] == null)  
            {  
                return RedirectToAction("Login");  
            } 
            _adminWebManager.UpdateQueuePatternItem(new QueuePatternItemVm()
            {
                Count = 0, ReceiverId = null, Priority = Dom.Queue.Priority.Medium,
                Category = Dom.Queue.Category.Partners
            });
            return RedirectToAction("QueuePatternItemsTable");
        }

        [HttpPost]
        public ActionResult ChangeQueuePatternItem(QueuePatternItemVm vm)
        {
            _adminWebManager.UpdateQueuePatternItem(vm);
            return RedirectToAction("QueuePatternItemsTable");
        }

        [HttpPost]
        public ActionResult DeleteQueuePatternItem(QueuePatternItemVm vm)
        {
            _adminWebManager.DeleteQueuePatternItem(vm);
            return RedirectToAction("QueuePatternItemsTable");
        }
        #endregion

        #region NodeRedactor

        public ActionResult NodesEditTable()
        {
            if (Session["UserID"] == null)  
            {  
                return RedirectToAction("Login");  
            } 
            var vm = _adminWebManager.GetNodesEditTable();
            return View("NodesEdit/NodesEditTable",vm);
        }

        [HttpPost]
        public ActionResult ChangeNodeItems(NodeEditListVm vm)
        {
            foreach (var item in vm.Items)
            {
                _adminWebManager.UpdateNodeItem(item);
            }
            return RedirectToAction("NodesEditTable");

        }
        public ActionResult Routes()
        {
            if (Session["UserID"] == null)  
            {  
                return RedirectToAction("Login");  
            } 
            return RedirectToAction("List", "Routes");
        }

        #endregion

        #region TrafficHistory

        public ActionResult GetWholeTrafficTable()
        {
            if (Session["UserID"] == null)  
            {  
                return RedirectToAction("Login");  
            } 
            var vm = _adminWebManager.GetWholeNodeTrafficTable();
            return View("Traffic/TrafficTable", vm);
        }

        public ActionResult GetCurrentTrafficTable()
        {
            if (Session["UserID"] == null)  
            {  
                return RedirectToAction("Login");  
            } 
            var vm = _adminWebManager.GetCurrentNodeTrafficTable();
            return View("Traffic/TrafficTable", vm);
        }

        #endregion

        #region Roles

        public ActionResult RolesEditor()
        {
            if (Session["UserID"] == null)  
            {  
                return RedirectToAction("Login");  
            } 
            var vm = _adminWebManager.GetRoleTableVm();
            return View("RolesEdit/RolesEditTable", vm);
        }

        public ActionResult AddNewRole()
        {
            if (Session["UserID"] == null)  
            {  
                return RedirectToAction("Login");  
            } 
            _adminWebManager.CreateRole(new RoleVm(){Assignments = new List<AssignmentVm>(), RoleName = "Роль"});
            return RedirectToAction("RolesEditor");
        }

        [HttpPost]
        public ActionResult ChangeRoleRecord(RoleVm vm)
        {
            _adminWebManager.UpdateRole(vm);
            return RedirectToAction("RolesEditor");
        }

        public ActionResult DeleteRoleRecord(long roleId)
        {
            if (Session["UserID"] == null)  
            {  
                return RedirectToAction("Login");  
            } 
            _adminWebManager.DeleteRole(roleId);
            return RedirectToAction("RolesEditor");
        }

        #endregion

        #region BlackList

        public ActionResult BlackListTable()
        {
            if (Session["UserID"] == null)  
            {  
                return RedirectToAction("Login");  
            } 
            return RedirectToAction("Records", "BlackList");
        }

        #endregion

        #region Dictionary update

        // http://localhost:3142/Admin/UpdateRecord?dictName=Employee&id=8c0fb04e-6345-44aa-ba4d-b192c819ffa3
        public string UpdateRecord(string dictName, string id)
        {
            var oneCApiClient = new OneCApiClient(GlobalConfigurationManager.OneCApiHost);
            string response = "None";
            object obj = null;
            switch (dictName)
            {
                case "Employee":
                    var employee = oneCApiClient.GetEmployee(id);
                    obj = employee;
                    _externalDataRepository.AddOrUpdate<ExternalData.Employee, string>(employee);
                    break;
                case "Contract":
                    var contract = oneCApiClient.GetContract(id);
                    obj = contract;
                    _externalDataRepository.AddOrUpdate<ExternalData.Contract, string>(contract);
                    break;
                case "Organisation":
                    var organisation = oneCApiClient.GetOrganisation(id);
                    obj = organisation;
                    _externalDataRepository.AddOrUpdate<ExternalData.Organisation, string>(organisation);
                    break;
                case "Partner":
                    var partner = oneCApiClient.GetPartner(id);
                    obj = partner;
                    _externalDataRepository.AddOrUpdate<ExternalData.Partner, string>(partner);
                    break;
                case "Product":
                    var product = oneCApiClient.GetProduct(id);
                    obj = product;
                    _externalDataRepository.AddOrUpdate<ExternalData.Product, string>(product);
                    break;
                case "Stock":
                    var stock = oneCApiClient.GetStock(id);
                    obj = stock;
                    _externalDataRepository.AddOrUpdate<ExternalData.Stock, string>(stock);
                    break;
                case "FixedAssets":
                    var fixedAssets = oneCApiClient.GetFixedAsset(id);
                    obj = fixedAssets;
                    _externalDataRepository.AddOrUpdate<ExternalData.FixedAsset, string>(fixedAssets);
                    break;
            }

            if (obj != null)
            {
                response = string.Empty;
                foreach(PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
                {
                    string name=descriptor.Name;
                    object value=descriptor.GetValue(obj);

                    response += $"{name} : {value}, ";
                }
            }
            return response;
        }

        #endregion

        #region Cards

        public ActionResult CardList(string queue = null)
        {
            if (Session["UserID"] == null)  
            {  
                return RedirectToAction("Login");  
            } 
            var list = _context.Cards.Where(c => queue == null || c.Id.Contains(queue)).ToList();
            return View(list);
        }
        
        [HttpGet]
        public ActionResult AddCard(string id = null)
        {
            if (Session["UserID"] == null)  
            {  
                return RedirectToAction("Login");  
            } 
            var card = new Card();
            if (id != null) card = _context.Cards.First(x => x.Id == id);
            return View(card);
        }
        
        [HttpPost]
        public ActionResult AddCard(Card card)
        {
            if (ModelState.IsValid)
            {
                _cardRepository.AddOrUpdate<Card, string>(card);
                return RedirectToAction("CardList");
            }
            return View(card);
        }

        public ActionResult DeleteCard(string id)
        {
            if (Session["UserID"] == null)  
            {  
                return RedirectToAction("Login");  
            } 
            var card = _context.Cards.FirstOrDefault(x => x.Id == id);
            if (card != null)
            {
                _cardRepository.Delete<Card, string>(card);
            }
            return RedirectToAction("CardList");
        }

        #endregion

        #region EmployeePhones

        public ActionResult GetEmployeePhones()
        {
            if (Session["UserID"] == null)  
            {  
                return RedirectToAction("Login");  
            } 
            var items = _adminWebManager.GetEmployeePhones();
            return View(items);
        }
        
        [HttpPost]
        public ActionResult EmployeePhonesUpdate(List<PhoneDictionary> model)
        {
            if (model is null) return RedirectToAction("GetEmployeePhones");
            var items = _adminWebManager.GetEmployeePhones().ToDictionary(x => x.Id);
            foreach (var employeePhone in model)
            {
                if (employeePhone.PhoneNumber.All(char.IsDigit) && employeePhone.PhoneNumber != items[employeePhone.Id].PhoneNumber)
                {
                    _adminWebManager.UpdateRolePhone(employeePhone);
                }
            }
            return RedirectToAction("GetEmployeePhones");
        }

        #endregion
    }
}