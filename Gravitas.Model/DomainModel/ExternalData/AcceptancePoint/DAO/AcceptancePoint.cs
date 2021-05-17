using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.AcceptancePoint.DAO
{
    public class ExternalData
    {
        public class AcceptancePoint : BaseEntity<string>
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public bool IsFolder { get; set; }
            public string ParentId { get; set; }
        }
    }
}