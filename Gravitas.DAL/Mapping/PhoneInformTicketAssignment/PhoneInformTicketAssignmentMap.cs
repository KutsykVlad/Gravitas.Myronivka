namespace Gravitas.DAL.Mapping.PhoneInformTicketAssignment
{
    class PhoneInformTicketAssignmentMap : BaseEntityMap<Model.DomainModel.PhoneInformTicketAssignment.DAO.PhoneInformTicketAssignment, long>
    {
        public PhoneInformTicketAssignmentMap()
        {
            ToTable("PhoneInformTicketAssignment");
        }
    }
}
