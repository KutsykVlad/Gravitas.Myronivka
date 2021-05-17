using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.ExternalUser.DTO
{
    public class ExternalEmployeeDetail : BaseEntity<string>
    {
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
    }
}