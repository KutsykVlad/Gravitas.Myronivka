namespace Gravitas.Infrastructure.Platform.Manager.Settings
{
    public interface ISettings
    {
        string PreRegistrationAdminEmail { get; set; }
        string AdminEmail { get; set; }
        string QueueDisplayText { get; set; }
        int LabDataExpireMinutes { get; set; }
        int QueueEntranceTimeout { get; set; }

        void Save();
    }
}