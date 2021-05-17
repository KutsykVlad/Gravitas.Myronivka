namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RouteNameLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RouteTemplate", "Name", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RouteTemplate", "Name", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
