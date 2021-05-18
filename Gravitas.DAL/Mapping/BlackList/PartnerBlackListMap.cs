using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.DAL.Mapping._Base;
using Gravitas.Model.DomainModel.BlackList.DAO;

namespace Gravitas.DAL.Mapping.BlackList
{
    class PartnerBlackListMap : BaseEntityMap<PartnersBlackListRecord, int>
    {
        public PartnerBlackListMap()
        {
            ToTable("blacklist.Partner");
            
            HasKey(list => list.Id);
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasRequired(p => p.Partner)
                .WithMany()
                .HasForeignKey(key => key.PartnerId);
        }
    }
}
