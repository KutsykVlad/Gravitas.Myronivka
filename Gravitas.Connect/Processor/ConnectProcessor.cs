using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gravitas.DAL;
using Gravitas.Infrastructure.Platform.ApiClient.SmsMobizon;
using Gravitas.Model;

namespace Gravitas.Connect.Processor
{
	class ConnectProcessor
	{
		private readonly IDictionary<Guid, Task> _connectTasks = new ConcurrentDictionary<Guid, Task>();

		private bool AddTask(Guid msgGuid, Task task)
		{
			if (_connectTasks.ContainsKey(msgGuid)
				|| _connectTasks.Count > 50) {
				return false;
			}

			_connectTasks.Add(msgGuid, task);
			return true;
		}

		public void CheckTaskList() {
			foreach (KeyValuePair<Guid, Task> task in _connectTasks.ToList()) {
				if (task.Value.IsCompleted || task.Value.IsCanceled || task.Value.IsFaulted)
					_connectTasks.Remove(task.Key);
			}
		}
	}
}
