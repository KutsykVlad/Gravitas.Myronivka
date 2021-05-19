using System;
using System.Collections.Generic;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.Card;
using Gravitas.DAL.Repository.EmployeeRoles;
using Gravitas.DAL.Repository.ExternalData;
using Gravitas.Infrastructure.Platform.ApiClient.Devices;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Model.DomainModel.Card.DAO;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.EmployeeRoles.DAO;
using Gravitas.Model.DomainModel.EmployeeRoles.DTO;
using Gravitas.Platform.Web.ViewModel.Employee;
using CardType = Gravitas.Model.DomainValue.CardType;

namespace Gravitas.Platform.Web.Manager.Employee
{
    public class EmployeeWebManager : IEmployeeWebManager
    {
        private readonly ICardRepository _cardRepository;
        private readonly IEmployeeRolesRepository _employeeRolesRepository;
        private readonly IExternalDataRepository _externalDataRepository;
        private readonly IOpRoutineManager _opRoutineManager;
        private readonly GravitasDbContext _context;

        public EmployeeWebManager(IExternalDataRepository externalDataRepository,
            ICardRepository cardRepository, IOpRoutineManager opRoutineManager,
            IEmployeeRolesRepository employeeRolesRepository, 
            GravitasDbContext context)
        {
            _externalDataRepository = externalDataRepository;
            _cardRepository = cardRepository;
            _opRoutineManager = opRoutineManager;
            _employeeRolesRepository = employeeRolesRepository;
            _context = context;
        }

        public EmployeeListVm GetEmployeeList(string name = "", int pageNumber = 1, int pageSize = 25, int? roleId = null)
        {
            bool SearchFilter(Model.DomainModel.ExternalData.Employee.DAO.Employee item)
            {
                return IsContaining(item.FullName, name) || IsContaining(item.ShortName, name);
            }

            bool IsContaining(string mainStr, string searchStr)
            {
                return mainStr.ToUpper().Contains(searchStr != null ? searchStr.ToUpper() : string.Empty);
            }

            var items = new List<EmployeeDetailsVm>();
            int listCount;

            var isGuidPassed = Guid.TryParse(name, out _);
            if (isGuidPassed)
            {
                var e = _context.Employees.First(x => x.Id == name);
                listCount = 1;
                items.Add(new EmployeeDetailsVm
                {
                    Id = e.Id,
                    ShortName = e.ShortName,
                    FullName = e.FullName
                });
            }
            else
            {
                Dictionary<string, EmployeeRole> filteredEmployee = null;
                if (roleId != null)
                    filteredEmployee = _context.EmployeeRoles.Where(x => x.RoleId == roleId).ToDictionary(x => x.EmployeeId);

                listCount = _context.Employees.AsEnumerable().Count(x => SearchFilter(x) && (filteredEmployee == null || filteredEmployee.TryGetValue(x.Id, out _)));

                items = _context.Employees.AsEnumerable()
                    .Where(x =>
                        SearchFilter(x) && (filteredEmployee == null || filteredEmployee.TryGetValue(x.Id, out _)))
                    .OrderBy(item => item.Id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(item =>
                        new EmployeeDetailsVm
                        {
                            Id = item.Id, ShortName = item.ShortName, FullName = item.FullName
                        })
                    .ToList();
            }

            var lastPage = (int) Math.Ceiling(decimal.Divide(listCount, pageSize));

            return new EmployeeListVm
            {
                Items = items,
                PrevPage = Math.Max(pageNumber - 1, 1),
                NextPage = Math.Min(pageNumber + 1, lastPage),
                ItemsCount = listCount,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                Roles = _employeeRolesRepository.GetQuery<Role, int>().ToList()
            };
        }

        public EmployeeDetailsVm GetEmployeeDetails(string id)
        {
            var employee = _externalDataRepository.GetEmployeeDetail(id);
            var employeeCards = _context.Cards
                .Where(item => item.EmployeeId == employee.Id)
                .Select(item => item.Id)
                .ToList();

            var roles = _employeeRolesRepository.GetRoles().Items.Select(t => new EmployeeRoleVm
            {
                RoleId = t.RoleId, RoleName = t.Name, IsApplied = false
            }).ToList();

            var employeeRoles = _employeeRolesRepository.GetEmployeeRoles(id).Items.Select(t =>
                t.RoleId).ToList();

            var result = new EmployeeDetailsVm
            {
                Id = employee.Id,
                ShortName = employee.ShortName,
                FullName = employee.FullName,
                CardIds = employeeCards,
                Roles = new List<EmployeeRoleVm>()
            };

            foreach (var role in roles)
                if (!employeeRoles.Contains(role.RoleId))
                {
                    result.Roles.Add(role);
                }
                else
                {
                    role.IsApplied = true;
                    result.Roles.Add(role);
                }

            return result;
        }

        public void UpdateEmployeeRoles(EmployeeDetailsVm employee)
        {
            _employeeRolesRepository.ApplyEmployeeRoles(
                new RolesDto
                {
                    Items = employee.Roles.Where(t => t.IsApplied).Select(t => new RoleDetail
                    {
                        RoleId = t.RoleId
                    }).ToList()
                },
                employee.Id);
        }


        public (bool, string) AssignCardToEmployee(string employeeId)
        {
            if (_employeeRolesRepository.GetEmployeeRoles(employeeId).Items.Count < 1)
                return (false, @"Ви не можете прив'язати картку робітнику без ролей");

            // SingleWindowFirst table reader
            var rfidState = (RfidObidRwState) DeviceSyncManager.GetDeviceState(10000300);

            if (string.IsNullOrEmpty(rfidState.InData.Rifd)) return (false, @"У зчитувачі не виявлено картки");

            var card = _context.Cards.FirstOrDefault(e =>
                e.Id.Equals(rfidState.InData.Rifd, StringComparison.CurrentCultureIgnoreCase));

            if (card == null)
            {
                _cardRepository.Add<Card, string>(new Card
                {
                    Id = rfidState.InData.Rifd, IsActive = true, TypeId = CardType.EmployeeCard, EmployeeId = employeeId
                });
                return (true, @"Картка створена та при'вязана успішно");
            }

            if (!_opRoutineManager.IsRfidCardValid(out var cardErrMsg, card,
                CardType.EmployeeCard))
                return (false, cardErrMsg.Text);

            if (card.EmployeeId != null) return (false, @"Картка прив'язана до іншого робітника");

            card.EmployeeId = employeeId;
            _cardRepository.Update<Card, string>(card);
            return (true, @"Картка при'вязана успішно");
        }

        public (bool, string) UnAssignCardsFromEmployee(ICollection<string> cards)
        {
            var resultString = "";
            var result = true;
            foreach (var cardId in cards)
            {
                var card = _context.Cards.FirstOrDefault(e => e.Id.Equals(cardId));

                if (card == null)
                {
                    resultString += $"\nКартка {cardId} не зареєстрована у системі ";
                    result = false;
                    continue;
                }

                card.EmployeeId = null;
                _cardRepository.Update<Card, string>(card);
                resultString += $"\n Картка {cardId} відв'язана успішно ";
            }

            return (result, resultString);
        }
    }
}