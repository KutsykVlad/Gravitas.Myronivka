namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QueueTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QueueItemPriority",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Description = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.QueuePatternItem",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Count = c.Int(nullable: false),
                        CategoryId = c.Long(nullable: false),
                        PriorityId = c.Long(nullable: false),
                        PartnerId = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ext.Partner", t => t.PartnerId)
                .ForeignKey("dbo.QueueItemCategory", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.QueueItemPriority", t => t.PriorityId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.PriorityId)
                .Index(t => t.PartnerId);
            
            CreateTable(
                "dbo.QueueItemCategory",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Description = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QueuePatternItem", "PriorityId", "dbo.QueueItemPriority");
            DropForeignKey("dbo.QueuePatternItem", "CategoryId", "dbo.QueueItemCategory");
            DropForeignKey("dbo.QueuePatternItem", "PartnerId", "ext.Partner");
            DropIndex("dbo.QueuePatternItem", new[] { "PartnerId" });
            DropIndex("dbo.QueuePatternItem", new[] { "PriorityId" });
            DropIndex("dbo.QueuePatternItem", new[] { "CategoryId" });
            DropTable("dbo.QueueItemCategory");
            DropTable("dbo.QueuePatternItem");
            DropTable("dbo.QueueItemPriority");
        }
    }
}
