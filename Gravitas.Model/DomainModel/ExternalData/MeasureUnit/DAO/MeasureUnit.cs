using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.MeasureUnit.DAO
{
    public class MeasureUnit : BaseEntity<string>
    {
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public bool IsFolder { get; set; }
        public string ParentId { get; set; }
    }
}