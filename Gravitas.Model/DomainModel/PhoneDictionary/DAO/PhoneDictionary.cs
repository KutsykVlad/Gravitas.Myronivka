using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model
{
    public class PhoneDictionary : BaseEntity<int>
    {
        public string PhoneNumber { get; set; }
        public string EmployeePosition { get; set; }
        public bool IsVisibleForSingleWindow { get; set; }
    }
}
