namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMixedFeedSiloDeviceTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MixedFeedSilo", "DeviceId", "dbo.Device");
            DropIndex("dbo.MixedFeedSilo", new[] { "DeviceId" });
            CreateTable(
                "dbo.MixedFeedSiloDevice",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        MixedFeedSiloId = c.Long(nullable: false),
                        DeviceId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Device", t => t.DeviceId, cascadeDelete: true)
                .ForeignKey("dbo.MixedFeedSilo", t => t.MixedFeedSiloId, cascadeDelete: true)
                .Index(t => t.MixedFeedSiloId)
                .Index(t => t.DeviceId);
            
            DropColumn("dbo.MixedFeedSilo", "DeviceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MixedFeedSilo", "DeviceId", c => c.Long(nullable: false));
            DropForeignKey("dbo.MixedFeedSiloDevice", "MixedFeedSiloId", "dbo.MixedFeedSilo");
            DropForeignKey("dbo.MixedFeedSiloDevice", "DeviceId", "dbo.Device");
            DropIndex("dbo.MixedFeedSiloDevice", new[] { "DeviceId" });
            DropIndex("dbo.MixedFeedSiloDevice", new[] { "MixedFeedSiloId" });
            DropTable("dbo.MixedFeedSiloDevice");
            CreateIndex("dbo.MixedFeedSilo", "DeviceId");
            AddForeignKey("dbo.MixedFeedSilo", "DeviceId", "dbo.Device", "Id", cascadeDelete: true);
        }
    }
}
