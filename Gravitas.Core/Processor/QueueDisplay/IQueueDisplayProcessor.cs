using System.Threading;

namespace Gravitas.Core.Processor.QueueDisplay
{
    public interface IQueueDisplayProcessor
    {
        void Config(long deviceId);
        void ProcessLoop(CancellationToken token);
    }
}
