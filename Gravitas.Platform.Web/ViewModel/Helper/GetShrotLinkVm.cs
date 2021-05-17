using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel.Helper
{
    public class ShrotLink
    {
        public string Link { get; set; }
        public string Title { get; set; }
    }
    
    public class GetShrotLinkVm
    {
        public List<ShrotLink> Links { get; set; }
    }
}