using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainValue;

namespace Gravitas.Model.DomainModel.PhoneDictionary.DAO
{
    public class PhoneDictionary : BaseEntity<Phone>
    {
        public string PhoneNumber { get; set; }
        public string EmployeePosition { get; set; }
        public bool IsVisibleForSingleWindow { get; set; }
    }
}
