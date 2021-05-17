using System.Threading;

namespace Gravitas.Core.Manager.RfidZebraFx9500
{
    public interface IRfidZebraFx9500Manager
    {
        void SyncData(CancellationToken token);
    }
}