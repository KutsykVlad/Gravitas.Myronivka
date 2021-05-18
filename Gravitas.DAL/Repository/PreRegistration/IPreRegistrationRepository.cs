using System.Collections.Generic;
using Gravitas.DAL.DbContext;
using Gravitas.Model.DomainModel.PreRegistration.DAO;
using Gravitas.Model.DomainModel.PreRegistration.DTO;

namespace Gravitas.DAL.Repository.PreRegistration
{
    public interface IPreRegistrationRepository : IBaseRepository<GravitasDbContext>
    {
        IEnumerable<PreRegistrationProductVm> GetProducts();
        void AddProduct(PreRegisterProduct item);
        void RemoveProduct(long id);
        PreRegisterCompany FindCompanyByUserName(string email);
        bool AddOrUpdateCompany(PreRegisterCompany company);
        bool RemoveCompany(string email);
        bool IsRouteRegistered(long routeId);
    }
}