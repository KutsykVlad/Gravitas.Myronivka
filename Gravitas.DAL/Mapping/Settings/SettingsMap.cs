using System.ComponentModel.DataAnnotations.Schema;

namespace Gravitas.DAL.Mapping.Settings
{
    internal class SettingsMap : BaseEntityMap<Model.DomainModel.Settings.DAO.Settings, int>
    {
        public SettingsMap()
        {
            ToTable("Settings");
            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}