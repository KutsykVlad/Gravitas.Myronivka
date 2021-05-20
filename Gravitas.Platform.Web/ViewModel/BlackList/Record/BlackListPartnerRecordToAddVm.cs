using System.ComponentModel.DataAnnotations;
using Gravitas.Model.DomainModel.ExternalData.Partner.DTO.List;

namespace Gravitas.Platform.Web.ViewModel.BlackList.Record
{
    public class BlackListPartnerRecordToAddVm
    {
        public string Id { get; set; }
        [Required]
        public string Comment { get; set; }
        public PartnerItems Partners { get; set; }
    }
}