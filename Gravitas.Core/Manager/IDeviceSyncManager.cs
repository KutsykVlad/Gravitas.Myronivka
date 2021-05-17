namespace Gravitas.Core.Manager
{
    interface IDeviceSyncManager
    {
        bool IsAllFinished();
        void StartDevSyncTasks();
        void StopDevSyncTasks();
    }
}