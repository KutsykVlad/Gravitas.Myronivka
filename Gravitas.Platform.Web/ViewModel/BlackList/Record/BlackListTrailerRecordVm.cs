using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gravitas.Platform.Web.ViewModel
{
    public class BlackListTrailerRecordVm
    {
        public long Id { get; set; }
        [Required]
        [DisplayName("Номер трейлера")]
        public string TrailerNumber { get; set; }
        [Required]
        public string Comment { get; set; }
    }
}