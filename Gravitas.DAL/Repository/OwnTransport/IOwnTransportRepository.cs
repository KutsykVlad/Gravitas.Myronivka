using System.Collections.Generic;
using System.Linq;
using Gravitas.Model.DomainModel.OwnTransport.DTO;

namespace Gravitas.DAL.Repository.OwnTransport
{
    public interface IOwnTransportRepository
    {
        IQueryable<OwnTransportVm> GetList();
        bool Add(OwnTransportVm model);
        void Remove(long id);
    }
}