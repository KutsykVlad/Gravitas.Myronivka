using System.Threading;

namespace Gravitas.Core.Processor.QueueDisplay
{
    public interface IQueueDisplayProcessor
    {
        void Config(int deviceId);
        void ProcessLoop(CancellationToken token);
    }
}
