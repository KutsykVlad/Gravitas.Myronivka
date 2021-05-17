using System.ComponentModel.DataAnnotations;
using Gravitas.Model.DomainModel.ExternalData.AcceptancePoint.DTO.Detail;

namespace Gravitas.Platform.Web.ViewModel
{
    public class BlackListPartnerRecordToAddVm
    {
        public string Id { get; set; }
        [Required]
        public string Comment { get; set; }
        public ExternalData.PartnerItems Partners { get; set; }
    }
}