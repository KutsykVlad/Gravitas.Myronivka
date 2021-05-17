using System.Collections.Generic;
using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel.User {
    public class UserDetailsVm {
        public string Id { get; set; }
        
        [DisplayName("Ім'я")]
        public string ShortName { get; set; }
        
        [DisplayName("Повне ім'я")]
        public string FullName { get; set; }
        
        [DisplayName("Прив'язані картки")]
        public ICollection<string> CardIds { get; set; }
        
        public CardProcessingMsgVm CardProcessingMsgError { get; set; }
        
        public int ReturnPage { get; set; }
    }
}