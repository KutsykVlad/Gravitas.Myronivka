namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOwnTransportTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OwnTransport",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CardId = c.String(nullable: false, maxLength: 50),
                        TruckNo = c.String(nullable: false, maxLength: 20),
                        TrailerNo = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Card", t => t.CardId, cascadeDelete: true)
                .Index(t => t.CardId, unique: true)
                .Index(t => t.TruckNo, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OwnTransport", "CardId", "dbo.Card");
            DropIndex("dbo.OwnTransport", new[] { "TruckNo" });
            DropIndex("dbo.OwnTransport", new[] { "CardId" });
            DropTable("dbo.OwnTransport");
        }
    }
}
