using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel
{
    public class BlackListPartnerRecordVm
    {
        public string Id { get; set; }
        [DisplayName("Ім'я партнера")]
        public string PartnerName { get; set; }
        public string Comment { get; set; }
    }
}