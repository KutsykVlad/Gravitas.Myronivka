namespace Gravitas.DeviceSync.Manager {
	interface IDeviceSyncManager {

		bool IsAllFinished();
		void StartDevSyncTasks();
	    void StopDevSyncTasks();
	}
}
