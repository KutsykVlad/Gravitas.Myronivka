namespace Gravitas.Core.Manager.VkModuleSocket2
{
    public interface IVkModuleSocket2Manager
    {
        bool GetState(int deviceId);
        void SetState(int deviceId, bool value);
    }
}