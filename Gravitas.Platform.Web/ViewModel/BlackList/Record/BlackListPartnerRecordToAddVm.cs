using System.ComponentModel.DataAnnotations;

namespace Gravitas.Platform.Web.ViewModel
{
    public class BlackListPartnerRecordToAddVm
    {
        public string Id { get; set; }
        [Required]
        public string Comment { get; set; }
        public Model.Dto.ExternalData.PartnerItems Partners { get; set; }
    }
}