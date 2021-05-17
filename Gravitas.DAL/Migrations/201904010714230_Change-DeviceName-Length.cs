namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDeviceNameLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Device", "Name", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Device", "Name", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
