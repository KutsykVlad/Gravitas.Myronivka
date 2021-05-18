using System;
using System.Data.Entity;
using System.Linq;
using Gravitas.DAL.DbContext;

namespace Gravitas.Infrastructure.Platform.Manager.Settings
{
    public class Settings : ISettings
    {
        private readonly GravitasDbContext _context;
        public Settings(GravitasDbContext context)
        {
            _context = context;
            Update();
        }

        public string PreRegistrationAdminEmail { get; set; }
        public string AdminEmail { get; set; }
        public string QueueDisplayText { get; set; }
        public int LabDataExpireMinutes { get; set; }
        public int QueueEntranceTimeout { get; set; }

        private void Update()
        {
            var s = _context.Set.First();
            AdminEmail = s.AdminEmail;
            QueueDisplayText = s.QueueDisplayText;
            LabDataExpireMinutes = s.LabDataExpireMinutes;
            QueueEntranceTimeout = s.QueueEntranceTimeout;
            PreRegistrationAdminEmail = s.PreRegistrationAdminEmail;
        }
        
        public void Save()
        {
            var settings = _context.Set.First();
            if (settings == null) {
                throw new Exception("No settings");
            }

            settings.AdminEmail = AdminEmail;
            settings.QueueDisplayText = QueueDisplayText;
            settings.LabDataExpireMinutes = LabDataExpireMinutes;
            settings.QueueEntranceTimeout = QueueEntranceTimeout;
            settings.PreRegistrationAdminEmail = PreRegistrationAdminEmail;
                
            var entry = _context.Entry(settings);
            entry.CurrentValues.SetValues(settings);
            entry.State = EntityState.Modified;
            _context.SaveChanges();

            Update();
        }
    }
}