using System.Threading;
using System.Threading.Tasks;

namespace Gravitas.Core.Processor {

	public interface IOpRoutineProcessor {

		bool ValidateNodeConfig(Model.Dto.NodeConfig nodeConfig);

		void Config(long nodeId);

		void ReadDbData();
		Task ProcessLoop(CancellationToken token);
		void Process();
	}
}
