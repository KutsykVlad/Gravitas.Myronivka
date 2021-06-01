using System.Threading;
using System.Threading.Tasks;
using Gravitas.Model.DomainModel.Node.TDO.Json;

namespace Gravitas.Core.Processor.OpRoutine
{
    public interface IOpRoutineProcessor
    {
        void Config(int nodeId);

        void ReadDbData();
        Task ProcessLoop(CancellationToken token);
        void Process();
    }
}