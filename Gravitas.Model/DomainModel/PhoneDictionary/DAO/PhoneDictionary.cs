namespace Gravitas.Model
{
    public class PhoneDictionary : BaseEntity<long>
    {
        public string PhoneNumber { get; set; }
        public string EmployeePosition { get; set; }
        public bool IsVisibleForSingleWindow { get; set; }
    }
}
