namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSettingsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        AdminEmail = c.String(),
                        QueueDisplayText = c.String(),
                        LabDataExpireMinutes = c.Int(nullable: false),
                        QueueEntranceTimeout = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Settings");
        }
    }
}
