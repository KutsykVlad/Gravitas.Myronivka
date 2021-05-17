using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gravitas.DeviceSync.Manager.RfidZebraFx9500
{
	interface IRfidZebraFx9500Manager {

		void SyncData(CancellationToken token);
	}
}
