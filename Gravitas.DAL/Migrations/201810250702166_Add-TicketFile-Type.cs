namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTicketFileType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TicketFileType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.TicketFile", "TypeId", c => c.Long(nullable: false));
            CreateIndex("dbo.TicketFile", "TypeId");
            AddForeignKey("dbo.TicketFile", "TypeId", "dbo.TicketFileType", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TicketFile", "TypeId", "dbo.TicketFileType");
            DropIndex("dbo.TicketFile", new[] { "TypeId" });
            DropColumn("dbo.TicketFile", "TypeId");
            DropTable("dbo.TicketFileType");
        }
    }
}
