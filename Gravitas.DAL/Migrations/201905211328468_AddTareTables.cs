namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTareTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PackingTare",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Weight = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Title, unique: true);
            
            CreateTable(
                "dbo.TicketPackingTare",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TicketId = c.Long(nullable: false),
                        PackingTareId = c.Long(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PackingTare", t => t.PackingTareId, cascadeDelete: true)
                .ForeignKey("dbo.Ticket", t => t.TicketId, cascadeDelete: true)
                .Index(t => t.TicketId)
                .Index(t => t.PackingTareId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TicketPackingTare", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.TicketPackingTare", "PackingTareId", "dbo.PackingTare");
            DropIndex("dbo.TicketPackingTare", new[] { "PackingTareId" });
            DropIndex("dbo.TicketPackingTare", new[] { "TicketId" });
            DropIndex("dbo.PackingTare", new[] { "Title" });
            DropTable("dbo.TicketPackingTare");
            DropTable("dbo.PackingTare");
        }
    }
}
