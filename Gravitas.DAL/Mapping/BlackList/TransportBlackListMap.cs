using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gravitas.Model.DomainModel.BlackList.DAO;

namespace Gravitas.DAL.Mapping
{
    class TransportBlackListMap :  EntityTypeConfiguration<TransportBlackListRecord>
    {
        public TransportBlackListMap()
        {
            this.ToTable("blacklist.Transport");

            this.HasKey(list => list.Id);
            this.Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
