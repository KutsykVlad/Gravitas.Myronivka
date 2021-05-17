using System.Threading;
using Gravitas.Model;

namespace Gravitas.Core.Manager {

	public interface IBaseSyncManager
	{

		void SyncData(CancellationToken token);
	}
}