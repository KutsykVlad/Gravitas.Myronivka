using System;
using System.Collections.Generic;
using System.Linq;
using Gravitas.DAL;
using Gravitas.DAL.Repository.PreRegistration;
using Gravitas.Model.DomainModel.PreRegistration.DAO;
using Gravitas.PreRegistration.Api.Models;

namespace Gravitas.PreRegistration.Api.Managers.Queue
{
    public class QueueManager : IQueueManager
    {
        private readonly INodeRepository _nodeRepository;
        private readonly IPreRegistrationRepository _preRegistrationRepository;
        private readonly IOpDataRepository _opDataRepository;
        private readonly GravitasDbContext _context;

        public QueueManager(INodeRepository nodeRepository,
            IPreRegistrationRepository preRegistrationRepository,
            IOpDataRepository opDataRepository, 
            GravitasDbContext context)
        {
            _nodeRepository = nodeRepository;
            _preRegistrationRepository = preRegistrationRepository;
            _opDataRepository = opDataRepository;
            _context = context;
        }

        public List<ProductItem> GetAvailableProducts()
        {
            var result = _preRegistrationRepository.GetQuery<PreRegisterProduct, long>()
                .ToList()
                .Select(x => new ProductItem
                {
                    RouteTimeInMinutes = x.RouteTimeInMinutes,
                    RouteId = x.RouteTemplateId,
                    Title = x.Title,
                    BusyDateTimeList = _preRegistrationRepository.GetQuery<PreRegisterQueue, long>()
                        .Where(z => z.RouteTemplateId == x.RouteTemplateId)
                        .Select(z => z.RegisterDateTime)
                        .ToList(),
                    TrucksInQueue = _preRegistrationRepository.GetQuery<PreRegisterQueue, long>()
                        .Count(z => z.RouteTemplateId == x.RouteTemplateId)
                })
                .ToList();
            return result;
        }

        public (bool, string) AddToQueue(AddToQueueItem item, string userEmail)
        {
            var company = _preRegistrationRepository.FindCompanyByUserName(userEmail);
            if (company == null || !company.AllowToAdd)
            {
                return (false, @"Компанія не має дозволу на реєстрацію автомобілів.");
            }

            if (_preRegistrationRepository.GetQuery<PreRegisterQueue, long>().Count(x => x.PreRegisterCompanyId == company.Id) >= company.TrucksMax)
            {
                return (false, @"Усі доступні реєстрації для компанії використані."); ;
            }

            var registerQueue = _preRegistrationRepository.GetSingleOrDefault<PreRegisterQueue, long>(x => x.PhoneNo.Contains(item.PhoneNo));
            if (registerQueue != null)
            {
                return (false, $"Автомобіль за номером {item.PhoneNo} уже зареєстровано.");
            }

            var preRegisterProduct = _preRegistrationRepository.GetFirstOrDefault<PreRegisterProduct, long>(x => x.RouteTemplateId == item.RouteId);
            if (preRegisterProduct == null)
            {
                return (false, $"Не знайдено маршруту: {item.RouteId}.");
            }

            registerQueue = _preRegistrationRepository.GetFirstOrDefault<PreRegisterQueue, long>(x => x.RegisterDateTime == item.RegisterDateTime && x.RouteTemplateId == item.RouteId);
            if (registerQueue != null)
            {
                return (false, $"Дата {item.RegisterDateTime} вже зайнята.");
            }

            registerQueue = new PreRegisterQueue
            {
                PreRegisterCompanyId = company.Id,
                PhoneNo = item.PhoneNo,
                RouteTemplateId = preRegisterProduct.RouteTemplateId,
                RegisterDateTime = item.RegisterDateTime,
                TruckNumber = item.TruckNumber,
                Notice = item.Notice
            };

            _nodeRepository.Add<PreRegisterQueue, long>(registerQueue);

            return (true, item.RegisterDateTime.ToString());
        }

        public (bool, string) DeleteFromQueue(string phoneNo, string userEmail)
        {
            var company = _preRegistrationRepository.FindCompanyByUserName(userEmail);
            if (company == null || !company.AllowToAdd)
            {
                return (false, "Компанія не має дозволу видаляти автомобілі.");
            }

            var register = _opDataRepository.GetFirstOrDefault<PreRegisterQueue, long>(item => item.PhoneNo.Contains(phoneNo));
            if (register == null) return (false, $"Не знайдено автомобіля із номером {phoneNo}.");
            _opDataRepository.Delete<PreRegisterQueue, long>(register);

            return (true, "Автомобіль видалено.");
        }

        public OrganizationDetailsViewModel GetDetails(string userEmail)
        {
            var company = _preRegistrationRepository.FindCompanyByUserName(userEmail);
            if (company == null) throw new ArgumentException($"Не знайдено автомобілів для користувача: {userEmail}");

            return new OrganizationDetailsViewModel
            {
                IsApproved = company.AllowToAdd,
                TrucksAllowed = company.TrucksMax,
                ContactPhoneNumber = company.ContactPhoneNumber,
                EnterpriseCode = company.EnterpriseCode,
                Name = company.Name
            };
        }

        public (bool, string) UpdateDetails(string userEmail, CompanyInfo info)
        {
            var company = _preRegistrationRepository.FindCompanyByUserName(userEmail);
            if (company == null) throw new ArgumentException($"Не знайдено компанії: {userEmail}");

            company.ContactPhoneNumber = info.PhoneNo;
            company.EnterpriseCode = info.EnterpriseCode;
            company.Name = info.CompanyName;

            var result = _preRegistrationRepository.AddOrUpdateCompany(company);

            return result ? (true, @"Дані компанії оновлено.") : (false, @"Не вдалось оновити дані компанії.");
        }

        public IQueryable<PreRegisterTruck> GetTrucks(string userEmail)
        {
            var company = _preRegistrationRepository.FindCompanyByUserName(userEmail);
            if (company == null) throw new ArgumentException($"Не знайдено автомобілів для користувача: {userEmail}");

            var queueRecords = _context.PreRegisterQueues
                .Where(x => x.PreRegisterCompanyId == company.Id)
                .Select(x => new PreRegisterTruck
                {
                    Id = x.Id,
                    PhoneNo = x.PhoneNo,
                    RegisterDateTime = x.RegisterDateTime,
                    RouteTitle = _preRegistrationRepository.GetSingleOrDefault<PreRegisterProduct, long>(z => z.RouteTemplateId == x.RouteTemplateId, false)
                        .Title,
                    TruckNumber = x.TruckNumber,
                    Notice = x.Notice
                });
            return queueRecords;
        }
    }
}