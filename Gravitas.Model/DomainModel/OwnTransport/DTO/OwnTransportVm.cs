using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gravitas.Model.DomainModel.OwnTransport.DTO
{
    public class OwnTransportVm
    {
        [DisplayName("Номер картки")]
        public string Card { get; set; }
        
        [DisplayName("Номер автомобіля")]
        [Required]
        [MaxLength(20)]
        public string TruckNo { get; set; }

        [DisplayName("Номер причіпа")]
        public string TrailerNo { get; set; }

        public int Id { get; set; }
    }
}