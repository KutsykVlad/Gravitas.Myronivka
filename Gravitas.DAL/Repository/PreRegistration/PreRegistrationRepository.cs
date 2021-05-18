using System;
using System.Collections.Generic;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.Model;
using Gravitas.Model.DomainModel.PreRegistration.DAO;
using Gravitas.Model.DomainModel.PreRegistration.DTO;

namespace Gravitas.DAL.Repository.PreRegistration
{
    public class PreRegistrationRepository : BaseRepository<GravitasDbContext>, IPreRegistrationRepository
    {
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly GravitasDbContext _context;

        public PreRegistrationRepository(GravitasDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<PreRegistrationProductVm> GetProducts()
        {
            return GetQuery<PreRegisterProduct, long>()
                .ToList()
                .Select(x => new PreRegistrationProductVm
                {
                    Id = x.Id,
                    Title = x.Title,
                    Route = _context.RouteTemplates.First(z => z.Id == x.RouteTemplateId).Name,
                    RouteTimeInMinutes =  x.RouteTimeInMinutes
                });
        }

        public void AddProduct(PreRegisterProduct item)
        {
            AddOrUpdate<PreRegisterProduct, long>(item);
        }

        public void RemoveProduct(long id)
        {
            var product = _context.PreRegisterProducts.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                Delete<PreRegisterProduct, long>(product);
            }
        }
        
        public PreRegisterCompany FindCompanyByUserName(string email)
        {
            var product = GetSingleOrDefault<PreRegisterCompany, long>(x => string.Equals(x.Email, email, StringComparison.CurrentCultureIgnoreCase));
            return product;
        }
        
        public bool AddOrUpdateCompany(PreRegisterCompany company)
        {
            try
            {
                AddOrUpdate<PreRegisterCompany, long>(company);
                return true;
            }
            catch (Exception e)
            {
                _logger.Error($"PreRegistration repository: AddOrUpdateCompany: Error on adding or updating company: {e}");
                return false;
            }
        }
        
        public bool RemoveCompany(string email)
        {
            try
            {
                var company = FindCompanyByUserName(email);
                if (company == null) return false;
                
                Delete<PreRegisterCompany, long>(company);
                return true;
            }
            catch (Exception e)
            {
                _logger.Error($"PreRegistration repository: RemoveCompany: Error on removing company: {e}");
                return false;
            }
        }

        public bool IsRouteRegistered(long routeId)
        {
            return GetSingleOrDefault<PreRegisterProduct, long>(x => x.RouteTemplateId == routeId) != null;
        }
    }
}