using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.Settings.DAO
{
    public class Settings : BaseEntity<int>
    {
        public string PreRegistrationAdminEmail { get; set; }
        public string AdminEmail { get; set; }
        public string QueueDisplayText { get; set; }
        public int LabDataExpireMinutes { get; set; }
        public int QueueEntranceTimeout { get; set; }
    }
}