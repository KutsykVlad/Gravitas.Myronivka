using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.PreRegistration.DAO
{
    [Table("PreRegisterCompany")]
    public class PreRegisterCompany : BaseEntity<int>
    {
        public string Email { get; set; }
        public bool AllowToAdd { get; set; }
        public int TrucksMax { get; set; }
        public int TrucksInProgress { get; set; }
        public string Name { get; set; }
        public string EnterpriseCode { get; set; }
        public string ContactPhoneNumber { get; set; }
    }
}