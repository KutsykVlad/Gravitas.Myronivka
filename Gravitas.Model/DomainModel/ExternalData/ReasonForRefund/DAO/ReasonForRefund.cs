using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.ReasonForRefund.DAO
{
    public class ReasonForRefund : BaseEntity<string>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsFolder { get; set; }
        public string ParentId { get; set; }
    }
}