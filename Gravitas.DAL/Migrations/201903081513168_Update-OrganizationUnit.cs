namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrganizationUnit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrganizationUnit", "ParentId", "dbo.OrganizationUnit");
            DropForeignKey("dbo.Node", "OrganisationUnitId", "dbo.OrganizationUnit");
            DropIndex("dbo.OrganizationUnit", new[] { "ParentId" });
            DropPrimaryKey("dbo.OrganizationUnit");
            AlterColumn("dbo.OrganizationUnit", "Id", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.OrganizationUnit", "Id");
            AddForeignKey("dbo.Node", "OrganisationUnitId", "dbo.OrganizationUnit", "Id");
            DropColumn("dbo.OrganizationUnit", "ParentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrganizationUnit", "ParentId", c => c.Long());
            DropForeignKey("dbo.Node", "OrganisationUnitId", "dbo.OrganizationUnit");
            DropPrimaryKey("dbo.OrganizationUnit");
            AlterColumn("dbo.OrganizationUnit", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.OrganizationUnit", "Id");
            CreateIndex("dbo.OrganizationUnit", "ParentId");
            AddForeignKey("dbo.Node", "OrganisationUnitId", "dbo.OrganizationUnit", "Id");
            AddForeignKey("dbo.OrganizationUnit", "ParentId", "dbo.OrganizationUnit", "Id");
        }
    }
}
