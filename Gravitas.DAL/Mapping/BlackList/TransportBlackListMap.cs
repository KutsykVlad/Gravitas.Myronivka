using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gravitas.DAL.Mapping
{
    class TransportBlackListMap :  EntityTypeConfiguration<Gravitas.Model.TransportBlackListRecord>
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
