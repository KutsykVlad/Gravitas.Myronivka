using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gravitas.Model.DomainModel.PackingTare.DTO
{
    public class PackingTareVm
    {
        public int Id { get; set; }
        
        [DisplayName("Назва")]
        [Required]
        public string Title { get; set; }
      
        [DisplayName("Вага")]
        public float Weight { get; set; }
    }
}
