using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.Crop.DTO
{
    public class CropDetail : BaseEntity<string>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}