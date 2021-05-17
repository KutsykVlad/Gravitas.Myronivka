namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Blacklisttransporttable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "blacklist.Transport",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TransportNo = c.String(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("blacklist.Driver", "Name", c => c.String());
            DropColumn("blacklist.Driver", "DriverName");
            DropColumn("blacklist.Driver", "DriverSurname");
        }
        
        public override void Down()
        {
            AddColumn("blacklist.Driver", "DriverSurname", c => c.String());
            AddColumn("blacklist.Driver", "DriverName", c => c.String());
            DropColumn("blacklist.Driver", "Name");
            DropTable("blacklist.Transport");
        }
    }
}
