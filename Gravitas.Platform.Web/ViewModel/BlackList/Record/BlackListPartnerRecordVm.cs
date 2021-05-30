using System;
using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel.BlackList.Record
{
    public class BlackListPartnerRecordVm
    {
        public Guid Id { get; set; }
        [DisplayName("Ім'я партнера")]
        public string PartnerName { get; set; }
        public string Comment { get; set; }
    }
}