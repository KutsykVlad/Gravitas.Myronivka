using System.ComponentModel.DataAnnotations.Schema;

namespace Gravitas.DAL.Mapping
{
    class PartnerBlackListMap : BaseEntityMap<Gravitas.Model.PartnersBlackListRecord, long>
    {
        public PartnerBlackListMap()
        {
            this.ToTable("blacklist.Partner");
            
            this.HasKey(list => list.Id);
            this.Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasRequired(p => p.Partner)
                .WithMany()
                .HasForeignKey(key => key.PartnerId);
        }
    }
}
