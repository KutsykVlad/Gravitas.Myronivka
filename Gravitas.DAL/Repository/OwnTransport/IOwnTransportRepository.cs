using System.Collections.Generic;
using Gravitas.DAL.Repository.OwnTransport.Models;

namespace Gravitas.DAL.Repository.OwnTransport
{
    public interface IOwnTransportRepository
    {
        List<OwnTransportViewModel> GetList();
        void AddOrUpdate(OwnTransportViewModel model);
        void Remove(int id);
        OwnTransportViewModel GetById(int id);
    }
}