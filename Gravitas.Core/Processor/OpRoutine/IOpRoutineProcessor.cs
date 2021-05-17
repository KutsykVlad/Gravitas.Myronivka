using System.Threading;
using System.Threading.Tasks;
using Gravitas.Model.DomainModel.Node.TDO.Json;

namespace Gravitas.Core.Processor {

	public interface IOpRoutineProcessor {

		bool ValidateNodeConfig(NodeConfig nodeConfig);

		void Config(long nodeId);

		void ReadDbData();
		Task ProcessLoop(CancellationToken token);
		void Process();
	}
}
