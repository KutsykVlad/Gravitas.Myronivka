using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Manager
{
    public interface INodeWebManager
    {
        NodeProgresVm GetNodeProgress(long id);
    }
}