using System;

namespace Gravitas.Platform.Web.ViewModel
{
    public class PrintoutOpVisaItemVm
    {
        public long Id { get; set; }
        public DateTime? DateTime { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
    }
}