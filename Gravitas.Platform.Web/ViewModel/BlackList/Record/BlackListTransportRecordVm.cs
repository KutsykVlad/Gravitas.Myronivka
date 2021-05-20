using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gravitas.Platform.Web.ViewModel.BlackList.Record
{
    public class BlackListTransportRecordVm
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Номер транспорту")]
        public string TransportNumber { get; set; }
        [Required]
        public string Comment { get; set; }
    }
}