namespace Gravitas.PreRegistration.Api.Models
{
    public class OrganizationDetailsViewModel
    {
        public bool IsApproved { get; set; }
        public int TrucksAllowed { get; set; }
        public string Name { get; set; }
        public string EnterpriseCode { get; set; }
        public string ContactPhoneNumber { get; set; }
    }
}