namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTicketFileTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TicketFile",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        FilePath = c.String(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        TicketId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ticket", t => t.TicketId, cascadeDelete: true)
                .Index(t => t.TicketId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TicketFile", "TicketId", "dbo.Ticket");
            DropIndex("dbo.TicketFile", new[] { "TicketId" });
            DropTable("dbo.TicketFile");
        }
    }
}
