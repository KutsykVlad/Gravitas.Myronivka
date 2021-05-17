namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmergencyFlagNode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Node", "IsEmergency", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Node", "IsEmergency");
        }
    }
}
