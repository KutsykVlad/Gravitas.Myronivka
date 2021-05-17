using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.YearOfHarvest.DTO
{
    public class YearOfHarvestDetail : BaseEntity<string>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}