using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.Model.DomainModel.BlackList.DAO;

namespace Gravitas.DAL.Mapping
{
    class PartnerBlackListMap : BaseEntityMap<PartnersBlackListRecord, long>
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
