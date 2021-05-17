using System.Threading.Tasks;

namespace Gravitas.Infrastructure.Service
{
	public interface IServiceManager {

		Task Cancel();
		void Batch();
	}
}
