using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Manager.Node
{
    public interface INodeWebManager
    {
        NodeProgresVm GetNodeProgress(int id);
    }
}