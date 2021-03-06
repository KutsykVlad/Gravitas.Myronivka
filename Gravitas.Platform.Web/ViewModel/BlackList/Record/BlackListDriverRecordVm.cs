using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gravitas.Platform.Web.ViewModel.BlackList.Record
{
    public class BlackListDriverRecordVm
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Прізвище водія")]
        public string Surname { get; set; }
        [Required]
        public string Comment { get; set; }
    }
}