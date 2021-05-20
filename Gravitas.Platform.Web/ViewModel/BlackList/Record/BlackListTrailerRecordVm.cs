using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gravitas.Platform.Web.ViewModel.BlackList.Record
{
    public class BlackListTrailerRecordVm
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Номер трейлера")]
        public string TrailerNumber { get; set; }
        [Required]
        public string Comment { get; set; }
    }
}