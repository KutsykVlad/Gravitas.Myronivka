namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLastNodeIdToTicket : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ticket", "NodeId", c => c.Long());
            CreateIndex("dbo.Ticket", "NodeId");
            AddForeignKey("dbo.Ticket", "NodeId", "dbo.Node", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ticket", "NodeId", "dbo.Node");
            DropIndex("dbo.Ticket", new[] { "NodeId" });
            DropColumn("dbo.Ticket", "NodeId");
        }
    }
}
