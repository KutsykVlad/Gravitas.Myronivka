using System.Collections.Generic;
using Gravitas.DAL.Repository._Base;
using Gravitas.Model.DomainModel.PreRegistration.DAO;
using Gravitas.Model.DomainModel.PreRegistration.DTO;

namespace Gravitas.DAL.Repository.PreRegistration
{
    public interface IPreRegistrationRepository : IBaseRepository
    {
        IEnumerable<PreRegistrationProductVm> GetProducts();
        void AddProduct(PreRegisterProduct item);
        void RemoveProduct(int id);
        PreRegisterCompany FindCompanyByUserName(string email);
        bool AddOrUpdateCompany(PreRegisterCompany company);
        bool RemoveCompany(string email);
        bool IsRouteRegistered(int routeId);
    }
}