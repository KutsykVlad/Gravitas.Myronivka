namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SMSField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QueueRegister", "IsSMSSend", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QueueRegister", "IsSMSSend");
        }
    }
}
