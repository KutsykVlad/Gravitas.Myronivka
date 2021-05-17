namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDateTimeColumns : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RouteTemplate", "UpdatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.RouteTemplate", "CreatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RouteTemplate", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.RouteTemplate", "UpdatedAt", c => c.DateTime(nullable: false));
        }
    }
}
