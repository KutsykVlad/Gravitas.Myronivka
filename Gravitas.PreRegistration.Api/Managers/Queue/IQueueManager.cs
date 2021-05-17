using System.Collections.Generic;
using System.Linq;
using Gravitas.PreRegistration.Api.Models;

namespace Gravitas.PreRegistration.Api.Managers.Queue
{
    public interface IQueueManager
    {
        List<ProductItem> GetAvailableProducts();
        (bool, string) AddToQueue(AddToQueueItem item, string userEmail);
        (bool, string) DeleteFromQueue(string phoneNo, string userEmail);
        OrganizationDetailsViewModel GetDetails(string userEmail);
        IQueryable<PreRegisterTruck> GetTrucks(string userEmail);
        (bool, string) UpdateDetails(string userEmail, CompanyInfo info);
    }
}