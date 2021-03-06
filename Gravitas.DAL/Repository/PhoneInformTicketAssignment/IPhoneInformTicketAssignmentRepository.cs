using System.Linq;

namespace Gravitas.DAL.Repository.PhoneInformTicketAssignment
{
    public interface IPhoneInformTicketAssignmentRepository
    {
        void Add(Model.DomainModel.PhoneInformTicketAssignment.DAO.PhoneInformTicketAssignment model);
        void Delete(int id);
        IQueryable<Model.DomainModel.PhoneInformTicketAssignment.DAO.PhoneInformTicketAssignment> GetAll();
        Model.DomainModel.PhoneInformTicketAssignment.DAO.PhoneInformTicketAssignment GetById(int id);
    }
}
