namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEndPointNodetable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EndPointNode",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NodeId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Node", t => t.NodeId, cascadeDelete: true)
                .Index(t => t.NodeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EndPointNode", "NodeId", "dbo.Node");
            DropIndex("dbo.EndPointNode", new[] { "NodeId" });
            DropTable("dbo.EndPointNode");
        }
    }
}
