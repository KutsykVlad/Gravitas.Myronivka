namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPreRegisterQueue : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PreRegisterQueue",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PhoneNo = c.String(),
                        PreRegisterCompanyId = c.Long(nullable: false),
                        RouteTemplateId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PreRegisterCompany", t => t.PreRegisterCompanyId, cascadeDelete: true)
                .ForeignKey("dbo.RouteTemplate", t => t.RouteTemplateId, cascadeDelete: true)
                .Index(t => t.PreRegisterCompanyId)
                .Index(t => t.RouteTemplateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PreRegisterQueue", "RouteTemplateId", "dbo.RouteTemplate");
            DropForeignKey("dbo.PreRegisterQueue", "PreRegisterCompanyId", "dbo.PreRegisterCompany");
            DropIndex("dbo.PreRegisterQueue", new[] { "RouteTemplateId" });
            DropIndex("dbo.PreRegisterQueue", new[] { "PreRegisterCompanyId" });
            DropTable("dbo.PreRegisterQueue");
        }
    }
}
