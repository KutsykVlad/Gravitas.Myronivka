namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTechnologicalRouteFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RouteTemplate", "IsTechnological", c => c.Boolean(nullable: false));
            AddColumn("opd.SingleWindowOpData", "OrganizationTitle", c => c.String());
            AddColumn("opd.SingleWindowOpData", "ReceiverTitle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("opd.SingleWindowOpData", "ReceiverTitle");
            DropColumn("opd.SingleWindowOpData", "OrganizationTitle");
            DropColumn("dbo.RouteTemplate", "IsTechnological");
        }
    }
}
