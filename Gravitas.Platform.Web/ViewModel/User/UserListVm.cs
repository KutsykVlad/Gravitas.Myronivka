using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel.User {
    public class UserListVm {
        public IEnumerable<UserDetailsVm> Items { get; set; }
        
        public int PrevPage { get; set; }
        public int NextPage { get; set; }
        public int ItemsCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}