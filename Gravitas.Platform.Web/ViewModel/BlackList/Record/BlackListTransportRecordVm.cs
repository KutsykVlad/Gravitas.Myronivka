using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gravitas.Platform.Web.ViewModel
{
    public class BlackListTransportRecordVm
    {
        public long Id { get; set; }
        [Required]
        [DisplayName("Номер транспорту")]
        public string TransportNumber { get; set; }
        [Required]
        public string Comment { get; set; }
    }
}