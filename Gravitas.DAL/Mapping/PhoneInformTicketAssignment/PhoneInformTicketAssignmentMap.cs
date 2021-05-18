using Gravitas.DAL.Mapping._Base;

namespace Gravitas.DAL.Mapping.PhoneInformTicketAssignment
{
    class PhoneInformTicketAssignmentMap : BaseEntityMap<Model.DomainModel.PhoneInformTicketAssignment.DAO.PhoneInformTicketAssignment, int>
    {
        public PhoneInformTicketAssignmentMap()
        {
            ToTable("PhoneInformTicketAssignment");
        }
    }
}
