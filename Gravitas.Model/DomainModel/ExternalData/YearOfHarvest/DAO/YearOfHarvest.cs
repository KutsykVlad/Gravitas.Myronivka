using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.YearOfHarvest.DAO
{
    public class YearOfHarvest : BaseEntity<string>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsFolder { get; set; }
        public string ParentId { get; set; }
    }
}