using System.Threading;

namespace Gravitas.Core.Manager
{
    public interface IBaseSyncManager
    {
        void SyncData(CancellationToken token);
    }
}