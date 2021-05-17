namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTareTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PackingTareMap",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Weight = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Title, unique: true);
            
            CreateTable(
                "dbo.TicketPackingTareMap",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PackingTareId = c.Long(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PackingTareMap", t => t.PackingTareId, cascadeDelete: true)
                .Index(t => t.PackingTareId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TicketPackingTareMap", "PackingTareId", "dbo.PackingTareMap");
            DropIndex("dbo.TicketPackingTareMap", new[] { "PackingTareId" });
            DropIndex("dbo.PackingTareMap", new[] { "Title" });
            DropTable("dbo.TicketPackingTareMap");
            DropTable("dbo.PackingTareMap");
        }
    }
}
